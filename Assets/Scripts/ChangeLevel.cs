using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;      // don't forget this
                                        // scenes must be added to the build index
                                        
public class ChangeLevel : MonoBehaviour
{
    [Tooltip("The name of the level you want to go to.")]
    public string destination = "Level 2";

    public void ChangeScene() {
        SceneManager.LoadScene(destination);
        PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        player.startPosition = GameObject.Find("Start Here").transform.position;
        player.ResetPlayer();
    }

    void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")) {
            ChangeScene();
        }
    }
}
