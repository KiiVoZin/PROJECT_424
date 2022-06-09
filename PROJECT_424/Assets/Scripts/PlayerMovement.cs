using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Reference to player controller
    [SerializeField] Rigidbody rb;
    //Player max speed
    public float speed;
    //Not important

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized * speed;
        if(direction.magnitude >= 0.1f){
        rb.AddForce(direction * Time.deltaTime);
        }
        if(rb.velocity.magnitude > speed){
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, speed);
        }
    }
}
