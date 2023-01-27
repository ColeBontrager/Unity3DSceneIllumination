using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserMovement : MonoBehaviour
{
    
    private Rigidbody rb;
    private Vector3 moveDir;
    [SerializeField] float walkSpeed;
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
    }

    //rotates camera based on mouse position
    //speed of rotation determined by sensitivity variable
    void Update()
    {
        

        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveDir = (x * transform.right + y * transform.forward).normalized;

    }

    void FixedUpdate()
    {
        float speed = walkSpeed;
        Vector3 grav = new Vector3(0, rb.velocity.y, 0);
        rb.velocity = moveDir * speed * Time.deltaTime;
        rb.velocity += grav;


    }
}
