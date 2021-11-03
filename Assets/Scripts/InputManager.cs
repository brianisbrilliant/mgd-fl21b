using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // get a reference to the playerMovement script
    public PlayerMovement player;

    public bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        if(player == null) {
            player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        }

        if(pauseMenu == null) pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(paused);
    }   

    public GameObject pauseMenu;

    void Pause() {
        paused = !paused;

        if(paused) {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        } else {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            Pause();
        }

        if(Input.GetKey(KeyCode.LeftControl)) {
            if(Input.GetKeyDown(KeyCode.Q)) {
                Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false;
            }
        }


        // call movement every frame and send it axis data.
        player.dir.x = Input.GetAxis("Horizontal");
        player.dir.z = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.Space)) {
            player.Jump();
        }

        if(Input.GetKey(KeyCode.LeftShift)) {
            player.Dash();
        }

        if(Input.GetKeyDown(KeyCode.F5)) {
            // resetJump
            PlayerPrefs.SetInt("canJump", 0);
        }
    }
}
