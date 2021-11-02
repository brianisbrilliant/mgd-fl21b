using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    public PlayerMovement player;

    public float heightAboveGround = 0.1f;

    Vector3 arrowScale;             // reference "size" for the arrow

    void Start() {
        arrowScale = Vector3.one;   // set the reference size to 1,1,1
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.rotation = Quaternion.LookRotation(player.dir, Vector3.up);
        arrowScale.z = player.dir.magnitude;        // update the depth scale based on the strength of our movement
        this.transform.localScale = arrowScale;     // assign the scale to the arrow.
        if(this.transform.position.y != heightAboveGround) {
            Vector3 newPos = new Vector3(transform.position.x,
                                        heightAboveGround,
                                        transform.position.z);
            this.transform.position = newPos;
        }
    }
}
