using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightData : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    [SerializeField] Transform firstCamera;
    [SerializeField] Transform staticCamera;
    [SerializeField] Transform light1;
    [SerializeField] Vector3 light1Ambient;
    [SerializeField] Vector3 light1Diffuse;
    [SerializeField] Vector3 light1Specular;
    [SerializeField] float light1Attenuation;
    private int light1Active = 1;
    [SerializeField] Transform light2;
    [SerializeField] Vector3 light2Ambient;
    [SerializeField] Vector3 light2Diffuse;
    [SerializeField] Vector3 light2Specular;
    [SerializeField] float light2Attenuation;
    private int light2Active = 1;
    [SerializeField] RollingBall rolling;
    [SerializeField] GameObject helpScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(light1Active == 1)
            {
                light1Active = 0;
                light1.gameObject.SetActive(false);
            }
            else
            {
                light1Active = 1;
                light1.gameObject.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (light2Active == 1)
            {
                light2Active = 0;
                light2.gameObject.SetActive(false);
            }
            else
            {
                light2Active = 1;
                light2.gameObject.SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            if(helpScreen.active)
            {
                helpScreen.SetActive(false);
            }
            else
            {
                helpScreen.SetActive(true);
            }
        }
        PassLightData();
    }

    void PassLightData()
    {
        float torchAtten = Mathf.PerlinNoise(0, Time.time / 2) * 2 - 1f;
        foreach (GameObject obj in objects)
        {
            Material[] mats = obj.GetComponent<MeshRenderer>().materials;
            foreach (Material mat in mats)
            {
                //set light1 values
                mat.SetVector("_Light1Ambient", light1Ambient);
                mat.SetVector("_Light1Diffuse", light1Diffuse);
                mat.SetVector("_Light1Position", light1.position);
                mat.SetVector("_Light1Specular", light1Specular);
                mat.SetFloat("_Light1Attenuation", light1Attenuation);
                mat.SetFloat("_Light1Active", light1Active);
                //set light 2 values
                mat.SetVector("_Light2Ambient", light2Ambient);
                mat.SetVector("_Light2Diffuse", light2Diffuse);
                mat.SetVector("_Light2Position", light2.position);
                mat.SetVector("_Light2Specular", light2Specular);
                mat.SetFloat("_Light2Attenuation", light2Attenuation + torchAtten);
                mat.SetFloat("_Light2Active", light2Active);
                if (rolling.firstPersonMode)
                {
                    mat.SetVector("_CameraPosition", firstCamera.position);
                }
                else
                {
                    mat.SetVector("_CameraPosition", staticCamera.position);
                }
            }
            
        }
    }
}
