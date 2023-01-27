using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBall : MonoBehaviour
{
    [SerializeField] GameObject firstPersonCamera;
    [SerializeField] GameObject staticCamera;
    [SerializeField] GameObject canvas;
    public bool firstPersonMode = true;
    private Vector2 startPos = new Vector2();
    private Vector2 delta = new Vector2();
    [SerializeField] float r;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !firstPersonMode)
        {
            ActivateFirstPersonCam();
        }
        else
        {
            float dr = delta.magnitude;
            Vector2 n = new Vector2(delta.y, -delta.x).normalized;

            float angle = Mathf.Asin(dr / Mathf.Sqrt(r * r + dr * dr)) * .22f;
            transform.RotateAround(transform.position, n, angle);
        }
        
    }

    void OnMouseDown()
    {
        if(firstPersonMode)
        {
            ActivateStaticCam();
        }
        else
        {
            startPos = Input.mousePosition;
        }
        
    }

    void OnMouseDrag()
    {
        if(!firstPersonMode)
        {
            Vector2 endPos = Input.mousePosition;
            delta = endPos - startPos;
            startPos = endPos;
        }
    }

    void OnMouseUp()
    {
        delta = new Vector2();
    }

    void ActivateStaticCam()
    {
        firstPersonCamera.SetActive(false);
        canvas.SetActive(false);
        staticCamera.SetActive(true);
        firstPersonMode = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void ActivateFirstPersonCam()
    {
        firstPersonCamera.SetActive(true);
        canvas.SetActive(true);
        staticCamera.SetActive(false);
        firstPersonMode = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
