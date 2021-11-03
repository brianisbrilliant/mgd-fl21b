using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{   
    public UIController ui;
    public Camera mainCam;          // attach the main camera here.

    [Tooltip("Speed multiplier for Horizontal and Vertical movement.")]
    [Range(5f,50f)]                             // adds a slider to drag
    public float speed = 10, jumpForce = 5, dashForce = 10;
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
        if(mainCam == null) mainCam = GameObject.Find("Main Camera").GetComponent<Camera>();
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

    public void Dash() {
        if(canDash) {
            Debug.Log("Absolutely Dashing!");
            // optionally, cancel out velocity to move in new direction
            rb.velocity = Vector3.zero;
            rb.AddForce(dir * dashForce, ForceMode.Impulse);
            StartCoroutine(Wait());
        }
    }

    bool canDash = true;

    IEnumerator Wait(float waitTime = 1f) {
        canDash = false;       // if true, now it is NOT true
        yield return new WaitForSeconds(waitTime);
        canDash = true;       // if false, now it is NOT false
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
        // make sure your cameras always face the same direction
        // make sure that the tunnel camera is the first child of the tunnel trigger
        // make sure this code is in OnTriggerExit() as well.
        else if(other.gameObject.CompareTag("AltCam")) {
            mainCam.gameObject.SetActive(false);
            other.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.gameObject.CompareTag("Floor")) {
            isGrounded = false;
        }
        else if(other.gameObject.CompareTag("AltCam")) {
            mainCam.gameObject.SetActive(true);
            other.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
