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
    [SerializeField] private HudCounter ammoCounter;
    [SerializeField] private HudCounter healthCounter;
    [SerializeField] private Transform playerPosTracker;
    [SerializeField] public Pool hitPFXPool;
    private TrackGameStats trackGameStats;

    public ObjectiveUI objectiveUI;

    public int initialNumDegenerators;
    public int numDestroyed = 0;
    public string degeneratorObjectiveName = "Destroy the Degenerators";

    private int numDeaths = 0;

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

        initialNumDegenerators = FindObjectsOfType<ObjectiveDeathBehavior>().Length;
        objectiveUI.NewObjective(degeneratorObjectiveName, " ( 0 / 4 )");

        trackGameStats = GetComponent<TrackGameStats>();
    }

    private void Update()
    {
        playerPosTracker.position = playerInstance.transform.position;
        playerPosTracker.rotation = playerInstance.transform.rotation;

        if (playerInstance.transform.position.y < -10.0f)
        {
            playerInstance.GetComponent<Health>().Damage(1000);
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

    public HudCounter GetAmmoCounter()
    {
        return ammoCounter;
    }

    public HudCounter GetHealthCounter()
    {
        return healthCounter;
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
        numDeaths++;
        trackGameStats.SetDeathsText(numDeaths);
        
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
