using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;

    private bool isPaused = false;

    private void Start()
    {
        SetPause(false);
    }

    public void SetPause(bool value)
    {
        if (value)
        {
            Cursor.lockState = CursorLockMode.None;

            Time.timeScale = 0.0f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;

            Time.timeScale = 1.0f;
        }
        
        pauseUI.SetActive(value);

        isPaused = value;
    }

    public bool GetIsPaused()
    {
        return isPaused;
    }
}
