// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Illumination"
{
    Properties
    {
        _Ambient ("Ambient", Color) = (1,1,1,1)
        _Diffuse ("Diffuse", Color) = (1,1,1,1)
        _Specular ("Specular", Color) = (1,1,1,1)
        _Shininess ("Shininess", Float) = 1
        _MainTex ("Texture", 2D) = "white" {}
        [MaterialToggle] _UseTex ("UseTexture", Float) = 0
    }
    SubShader
    {

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"


            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };
            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 n : NORMAL;
                float3 worldPos : TEXCOORD1;
                float2 uv : TEXCOORD0;
            };

            
            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                float3 N = UnityObjectToWorldNormal(v.normal);
                N = N / sqrt(N.x * N.x + N.y * N.y + N.z * N.z);
                o.n = N;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            //Material Attributes
            float4 _Ambient, _Diffuse, _Specular;
            
            float _Shininess, _UseTex;
            

            //Light1 Attributes
            float3 _Light1Ambient, _Light1Diffuse, _Light1Position, _Light1Specular;
            float _Light1Attenuation, _Light1Active;

            //Light2 Attributes
            float3 _Light2Ambient, _Light2Diffuse, _Light2Position, _Light2Specular;
            float _Light2Attenuation, _Light2Active;

            float3 _CameraPosition;

            

            fixed4 frag(v2f i) : SV_Target
            {
                //light1 light vector
                float3 L1 = (_Light1Position - i.worldPos);
                float d1 = sqrt(L1.x * L1.x + L1.y * L1.y + L1.z * L1.z);
                L1 = L1 / d1;

                //light2 light vector
                float3 L2 = (_Light2Position - i.worldPos);
                float d2 = sqrt(L2.x * L2.x + L2.y * L2.y + L2.z * L2.z);
                L2 = L2 / d2;

                float3 C = _CameraPosition - i.worldPos;
                C = C / sqrt(C.x * C.x + C.y * C.y + C.z * C.z);

                //light1 bisector vector
                float3 B1 = C + L1;
                B1 = B1 / sqrt(B1.x * B1.x + B1.y * B1.y + B1.z * B1.z);

                //light2 bisector vector
                float3 B2 = C + L2;
                B2 = B2 / sqrt(B2.x * B2.x + B2.y * B2.y + B2.z * B2.z);
                
                fixed4 texColor = tex2D(_MainTex, i.uv);
                float3 color1 = (0, 0, 0);
                float3 color2 = (0, 0, 0);
                //add ambient component
                if (_UseTex)
                {
                    color1 += _Light1Ambient * texColor;
                    color2 += _Light2Ambient * texColor;
                }
                else
                {
                    color1 += _Light1Ambient * _Ambient;
                    color2 += _Light2Ambient * _Ambient;
                }
                //add diffuse component
                if (_UseTex)
                {
                    color1 += max(0, dot(L1, i.n)) * _Light1Diffuse * texColor;
                    color2 += max(0, dot(L2, i.n)) * _Light2Diffuse * texColor;
                }
                else
                {
                    color1 += max(0, dot(L1, i.n)) * _Light1Diffuse * _Diffuse;
                    color2 += max(0, dot(L2, i.n)) * _Light2Diffuse * _Diffuse;
                }
                //add specular component
                float3 specular1 = _Light1Specular * _Specular * pow(max(0, dot(B1, i.n)), _Shininess);
                color1 += specular1;

                float3 specular2 = _Light2Specular * _Specular * pow(max(0, dot(B2, i.n)), _Shininess);
                color2 += specular2;
                
                //distance attenuation
                float attenuation1 = clamp(_Light1Attenuation / d1, 0.0, 1.0);
                color1 *= attenuation1;

                float attenuation2 = clamp(_Light2Attenuation / d2, 0.0, 1.0);
                color2 *= attenuation2;
                
                float3 color = (0, 0, 0);
                if (_Light1Active == 1)
                {
                    color += color1;
                }
                if (_Light2Active == 1)
                {
                    color += color2;
                }
                
                return fixed4 (color, 1);
            }
            ENDCG
        }
    }
}
