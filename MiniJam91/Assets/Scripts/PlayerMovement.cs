using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 200f;

    float horizontal;

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }
    private void FixedUpdate()
    {
        transform.Translate(new Vector3(horizontal,0,0) * Time.deltaTime * movementSpeed,Space.World);
    }
    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
