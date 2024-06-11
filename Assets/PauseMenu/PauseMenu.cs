using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] FirstPersonLook firstPersonLook; // Reference to the FirstPersonLook script

    public static bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f; // Pause the game
        isPaused = true;

        // Disable the FirstPersonLook script
        if (firstPersonLook != null)
        {
            firstPersonLook.enabled = false;
        }

        // Unlock and show the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        isPaused = false;

        // Enable the FirstPersonLook script
        if (firstPersonLook != null)
        {
            firstPersonLook.enabled = true;
        }

        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(0);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
