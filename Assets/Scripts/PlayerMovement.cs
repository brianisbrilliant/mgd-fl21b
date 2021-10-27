using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{   
    public UIController ui;

    [Tooltip("Speed multiplier for Horizontal and Vertical movement.")]
    [Range(5f,50f)]                             // adds a slider to drag
    public float speed = 10, jumpForce = 5;
    public float resetHeight = -5;

    public Vector3 dir;                         // this is the direction we want to add force
    public Vector3 startPosition;               // assign this in Start()

    public bool isGrounded = true;              // these don't need to be public 
    public bool canJump = false;

    private Rigidbody rb;                       // get a reference to a rigidbody.
    private int coins = 0;

    // make the player persist between scenes
    // void Awake() {
    //     DontDestroyOnLoad(this.gameObject);
    // }

    void Start() {
        rb = this.GetComponent<Rigidbody>();
        
        startPosition = GameObject.Find("Start Here").transform.position;
        if(PlayerPrefs.GetInt("canJump") == 1) {
            Debug.Log("We can jump!");
            canJump = true;
        }
        else {
            Debug.Log("canJump" + PlayerPrefs.GetInt("canJump"));
        }
        ResetPlayer();
        if(ui == null) ui = GameObject.Find("Canvas").GetComponent<UIController>();
    }

    void FixedUpdate() {
        rb.AddForce(dir * speed);

        // if the player falls below the level, reset the player
        if(this.transform.position.y < resetHeight) {
            ResetPlayer();
        }
    }

    public void ResetPlayer() {
        this.transform.position = startPosition;            // move player
        rb.velocity = Vector3.zero;                         // set speed to zero
        rb.angularVelocity = Vector3.zero;                  // set rotation speed to zero
        this.transform.rotation = Quaternion.identity;      // set rotation to 0,0,0.
    }

    public void Jump() {
        if(isGrounded && canJump) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Floor")) {
            isGrounded = true;
        }
        else if(other.gameObject.CompareTag("Coin")) {
            Destroy(other.gameObject);
            coins++;
            ui.AddScore();
        }
        else if(other.gameObject.CompareTag("JumpPowerup")) {
            canJump = true;
            PlayerPrefs.SetInt("canJump", 1);           // 1 is true, 0 is false
            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Floor")) {
            isGrounded = false;
        }
    }
}
