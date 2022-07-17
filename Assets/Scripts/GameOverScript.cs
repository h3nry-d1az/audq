using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Screen.fullScreen = !Screen.fullScreen;
        }

        if ((Input.GetKeyDown(KeyCode.LeftShift)) || (Input.GetKeyDown(KeyCode.RightShift)))
        {
            SceneManager.LoadScene("ControlsScene");
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("MainScene");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("StartScene");
        }
    }
}
