using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject playerInstance;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject respawnUI;
    [SerializeField] private ScaleFadeSeconds respawnSecondsDisplay;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        if (playerInstance == null)
        {
            Respawn();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        respawnUI.SetActive(false);
    }

    public GameObject GetPlayerInstance()
    {
        return playerInstance;
    }

    public void Respawn()
    {
        if (playerInstance != null)
        {
            Destroy(playerInstance.GetComponent<PlayerInput>());
        }
        
        playerInstance = Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
        playerInstance.GetComponent<PlayerInput>().ActivateInput();
    }

    public void Respawn(float delay)
    {
        StartCoroutine(DelayedRespawn(delay));
    }

    private IEnumerator DelayedRespawn(float delay)
    {
        respawnUI.SetActive(true);

        int seconds = Mathf.FloorToInt(delay);
        for (int i = 0; i < seconds; i++)
        {
            DisplayCountdown(seconds - i);
            yield return new WaitForSeconds(1.0f);
        }

        Respawn();

        respawnUI.SetActive(false);
    }

    private void DisplayCountdown(int secondsLeft)
    {
        if (secondsLeft > 0)
        {
            respawnSecondsDisplay.SetDigit(secondsLeft);
        }
    }
}
