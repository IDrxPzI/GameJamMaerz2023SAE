using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f; //speed of player
    [SerializeField] private float jumpForce = 6f; //jump force

    private Rigidbody rb; //rigidbody of player needed for physics
    private Transform cameraTransform; //for camera positioning

    private Vector3 input; //input of player
    private Vector3 velocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform; //tagged main camera
    }

    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        input.z = Input.GetAxis("Vertical"); //input for z axis
        input.x = Input.GetAxis("Horizontal"); //input for x axis

        input = Vector3.ClampMagnitude(input, 1); //clamping player speed. player can never walk faster than 1 

        Vector3 inputRelativeToCam = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0) * input; //camera movement

        velocity = inputRelativeToCam * speed; //Man könnte aber auch mit speed bei Horizontal und Vertical multiplizieren
        velocity.y = rb.velocity.y;

        //GroundCheck();

        if (Input.GetKeyDown(KeyCode.Space) && GroundCheck())
        {
            velocity.y = jumpForce; //jump
        }

        rb.velocity = velocity;
    }
    private bool GroundCheck() //if player is grounded or not
    {
        bool check = Physics.Raycast(rb.position, Vector3.down, 1.5f); //If jump not working ray too short

        return check; //returns true or false
    }
}
