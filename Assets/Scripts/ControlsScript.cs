using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsScript : MonoBehaviour
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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("StartScene");
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("MainScene");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SceneManager.LoadScene("ControlsScene2");
        }
    }
}
