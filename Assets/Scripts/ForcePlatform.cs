using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePlatform : MonoBehaviour
{
    public float force = 20f;
    public bool zeroOutVelocity = true;
    public bool singleUse = false;

    void OnDrawGizmosSelected()
    {
        // Draws a cyan line in the direction of the force.
        Gizmos.color = Color.cyan;
        Vector3 direction = transform.TransformDirection(Vector3.up) * force * .5f;
        Gizmos.DrawRay(transform.position, direction);
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            // get the rigidbody component of the player
            Rigidbody rb = other.GetComponent<Rigidbody>();
            // stop the player's movement 
            if(zeroOutVelocity) rb.velocity = Vector3.zero;
            // push the player UP relative to the direction of this platform
            rb.AddForce(this.transform.up * force, ForceMode.Impulse);
            // destroy this after use.
            if(singleUse) Destroy(this.gameObject);
        }
    }
}
