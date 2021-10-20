using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{   
    [Tooltip("Speed multiplier for Horizontal and Vertical movement.")]
    [Range(5f,50f)]           // adds a slider to drag
    public float speed = 10, jumpForce = 5;

    public Vector3 dir;     // this is the direction we want to add force
    public Vector3 startPosition;      // assign this in Start()

    public bool isGrounded = true;

    // get a reference to a rigidbody.
    Rigidbody rb;

    void Awake() {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start() {
        rb = this.GetComponent<Rigidbody>();
        startPosition = GameObject.Find("Start Here").transform.position;
        ResetPlayer();
    }

    void FixedUpdate() {
        rb.AddForce(dir * speed);

        // if the player falls below the level, reset the player
        if(this.transform.position.y < -5) {
            ResetPlayer();
        }
    }

    

    public void ResetPlayer() {
        this.transform.position = startPosition;        // move player
        rb.velocity = Vector3.zero;                     // set speed to zero
        rb.angularVelocity = Vector3.zero;              // set rotation speed to zero
        this.transform.rotation = Quaternion.identity;  // set rotation to 0,0,0.
    }

    public void Jump() {
        if(isGrounded) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    int coins = 0;

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Floor")) {
            isGrounded = true;
        }

        else if(other.gameObject.CompareTag("Coin")) {
            Destroy(other.gameObject);
            coins++;
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Floor")) {
            isGrounded = false;
        }
    }

    // // create a function to move.
    // public void MoveHorizontal(float force) {
    //     rb.AddForce(force * speed, 0, 0);
    // }

    // // create a function to move.
    // public void MoveVertical(float force) {
    //     rb.AddForce(0, 0, force * speed);
    // }
}
