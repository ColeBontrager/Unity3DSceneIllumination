using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] float speed;
    private bool moving = true;
    private int curIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[curIndex].position;
    }

    // Update is called once per frame
    void Update()
    {
        if(moving)
        {
            int targetPos = curIndex + 1;
            if (curIndex == points.Length - 1)
            {
                targetPos = 0;
            }
            transform.position = Vector3.MoveTowards(transform.position, points[targetPos].position, speed);

            if (Vector3.Distance(transform.position, points[targetPos].position) < .001f)
            {
                if (curIndex == points.Length - 1)
                {
                    curIndex = 0;
                }
                else
                {
                    curIndex++;
                }
            }
        }
        
    }

    void OnMouseDown()
    {
        moving = !moving;
    }
}
