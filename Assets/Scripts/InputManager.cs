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
        
    }

    // Update is called once per frame
    void Update()
    {
        // call movement every frame and send it axis data.
        player.MoveHorizontal(Input.GetAxis("Horizontal"));
        player.MoveVertical(Input.GetAxis("Vertical"));
    }
}
