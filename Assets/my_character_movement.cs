using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class my_character_movement : MonoBehaviour
{
    Rigidbody rb;
    public Animator my_animator;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float input_horizontal = rb.velocity.x;
        float input_vertical = rb.velocity.z;

        my_animator.SetFloat("InputX", input_horizontal);
        my_animator.SetFloat("InputY", input_vertical);


    }
}
