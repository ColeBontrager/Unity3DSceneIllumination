using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] int dir = 1;
    [SerializeField] float delta = 1.5f;
    private Quaternion startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rot = startPos;
        rot.z += dir * (delta * Mathf.Sin(Time.time * speed));
        transform.rotation = rot;
    }
}
