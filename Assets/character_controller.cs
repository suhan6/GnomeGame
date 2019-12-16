using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character_controller : MonoBehaviour
{
    Rigidbody rb;
    public float forwardspeed = 30.0f;
    public float strafespeed = 20.0f;
    public float backwardspeed = 15.0f;
    public float maxforwardspeed;
    public float actualMaxForwardSpeed;
    public float maxstrafespeed;
    public float maxbackwardspeed;
    public Animator animator;
    public float speedMultiply;
    public float BackwardsSpeedMultiply;
    public float turnSpeed;
    public float movementThreshhold;
    public float jumpForce = 2.0f;
    public bool isGrounded;
    public Vector3 jump;


    // Start is called before the first frame update
    void Start()
    {
        actualMaxForwardSpeed = maxforwardspeed;

        rb = GetComponent<Rigidbody>();

        jump = new Vector3(0.0f, 2.0f, 0.0f);

    }

    void OnCollisionStay(Collision target) {

        if(target.gameObject.tag.Equals("floor") == true) {

            isGrounded = true;
            actualMaxForwardSpeed = maxforwardspeed;

        }

        

    }

    // Update is called once per frame
    void Update()
    {

        //find the sideways input
        float x = Input.GetAxis("Horizontal"); // * speed;
        // apply stafe speed to x
        Vector3 xforce = new Vector3(x * strafespeed, 0f, 0f);
    
        //add the force 
        rb.AddForce(xforce, ForceMode.Force);
   


        //find the forwad/back input
        float z = Input.GetAxis("Vertical"); // * speed;

       
        //check if z is positive or negative and apply the appropriate speed multiplyer
        if (z > 0) 
        {
            Vector3 zforce = new Vector3(0.0f, 0.0f, forwardspeed);
            rb.AddForce(zforce, ForceMode.Force);
        }
        else if (z < 0) 
        {
            Vector3 zforce = new Vector3(0.0f, 0.0f, backwardspeed);
            rb.AddForce(zforce, ForceMode.Force);
        }
       
      
        // multiply by unit vector in z direction 
        

        //apply the force to the rigidbody
        

        //apply max speed constraints
        //Debug.Log(rb.velocity);
        //Debug.DrawLine(transform.position, transform.position + rb.velocity);

        float velocity_x = rb.velocity.x;
        float velocity_z = rb.velocity.z;

        velocity_x = Mathf.Clamp(velocity_x, maxstrafespeed * -1, maxstrafespeed);
        velocity_z = Mathf.Clamp(velocity_z, maxbackwardspeed, actualMaxForwardSpeed);

        rb.velocity = new Vector3(velocity_x, rb.velocity.y, velocity_z);

        //float h = Mathf.Sqrt(Mathf.Pow(velocity_x, 2) + Mathf.Pow(velocity_z, 2));
        Vector3 flattened_velocity = new Vector3(velocity_x, 0.0f, velocity_z);
        float flattened_vel_magnitude = flattened_velocity.magnitude;

        Vector3 current_flattened = transform.rotation * Vector3.forward;  // new Vector3(transform.rotation.eulerAngles.x, 0.0f, transform.rotation.eulerAngles.z);
        Debug.Log(current_flattened);
        Vector3 new_rotation = Vector3.LerpUnclamped(current_flattened, flattened_velocity, turnSpeed);

        transform.rotation = Quaternion.LookRotation(new_rotation);

        if (velocity_z > 0.0f) {
            animator.SetFloat("speedMultiplier", flattened_vel_magnitude * speedMultiply);
        } else if (velocity_z < 0.0f) {
            animator.SetFloat("speedMultiplier", flattened_vel_magnitude * BackwardsSpeedMultiply);
        }
        
        if (velocity_x <= movementThreshhold && velocity_z <= movementThreshhold && velocity_x >= -movementThreshhold && velocity_z >= -movementThreshhold) {
            //Debug.Log ("velocity = 0");
            animator.SetFloat("InputY", 0.0f);
            animator.SetFloat("speedMultiplier", 1.0f);
        } else {
            animator.SetFloat("InputY", 1.0f);
        }

     
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {

            actualMaxForwardSpeed = maxforwardspeed / 2;
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;

        }
    }

   
}
