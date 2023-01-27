using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaSurface : MonoBehaviour
{
    private Mesh mesh;
    [SerializeField] float size = 4;
    [SerializeField] int verts = 20;
    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uv;
    [SerializeField] float minHeight;
    [SerializeField] float maxHeight;

    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
    }

    // Update is called once per frame
    void Update()
    {
        GenerateMesh();
    }

    //Generates square mesh centered at origin
    //Height of vertices based on overlapping sin waves oscillating over time
    void GenerateMesh()
    {
        vertices = new Vector3[verts * verts];
        uv = new Vector2[vertices.Length];
        float vertDist = size / (verts-1);
        for (int v = 0, z = 0; z < verts; z++)
        {
            for (int x = 0; x < verts; x++)
            {
                float xCoord = x * vertDist;
                float zCoord = z * vertDist;
                float yCoord = ((Mathf.Sin(xCoord + Time.time) / 2f + Mathf.Sin(zCoord + Time.time) / 2f + 1) / 2f) * maxHeight + minHeight;
                vertices[v] = new Vector3(x * vertDist - (size / 2f), yCoord, z * vertDist - (size / 2f)) + transform.position;
                uv[v] = new Vector2((float)x / size, (float)z / size);
                v++;
            }
        }

        triangles = new int[((verts - 1) * 2) * (verts - 1) * 3];
        for (int tri = 0, vert = 0, z = 0; z < verts - 1; z++)
        {
            for (int x = 0; x < verts - 1; x++)
            {
                triangles[tri] = vert + 0;
                triangles[tri + 1] = vert + verts;
                triangles[tri + 2] = vert + 1;

                triangles[tri + 3] = vert + 1;
                triangles[tri + 4] = vert + verts;
                triangles[tri + 5] = vert + verts + 1;
                tri += 6;
                vert++;
            }
            vert++;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void OnDrawGizmos()
    {
        if (vertices != null)
        {
            foreach (Vector3 vert in vertices)
            {
                Gizmos.DrawSphere(vert, .1f);
            }
            Gizmos.DrawMesh(mesh);
        }
    }
}
