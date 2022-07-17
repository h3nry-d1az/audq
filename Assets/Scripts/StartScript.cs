using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Screen.fullScreen = true;
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

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("MainScene");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
    }
}
