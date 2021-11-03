using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // get a reference to the playerMovement script
    public PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null) {
            player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        }
    }   

    // Update is called once per frame
    void Update()
    {
        // call movement every frame and send it axis data.
        player.dir.x = Input.GetAxis("Horizontal");
        player.dir.z = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.Space)) {
            player.Jump();
        }

        if(Input.GetKey(KeyCode.LeftShift)) {
            player.Dash();
        }
    }
}
