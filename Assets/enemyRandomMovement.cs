using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyRandomMovement : MonoBehaviour
{
    private float latestDirectionChangeTime;
    public float directionChangeTime = 3f;
    public float speed = 2f;
    private Vector3 movementDirection;
    private Vector3 movementPerSecond;
    public Rigidbody rb;
    public float dampener;

    void Start() {
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
    }

    void calcuateNewMovementVector() {
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
        movementPerSecond = movementDirection * speed;
    }

    void Update() {
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime) {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();

            Vector3 force = new Vector3(movementPerSecond.x * Time.deltaTime, 0.0f, movementPerSecond.z * Time.deltaTime);

            Debug.Log(force);

            rb.AddForce(force / dampener, ForceMode.Impulse);
        }

        //move enemy: 
        
    }
}
