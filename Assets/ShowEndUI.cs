using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowEndUI : MonoBehaviour
{
    [SerializeField] private GameObject victoryUI;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI deathsText;

    private void Start()
    {
        victoryUI.SetActive(false);
    }

    public void ShowVictoryScreen()
    {
        TrackGameStats stats = GetComponent<TrackGameStats>();
        PauseMenu pause = GetComponent<PauseMenu>();
        pause.SetPause(false);
        pause.SetCanBePaused(false);

        victoryUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None;

        timeText.text = stats.GetTotalTimeString();
        deathsText.text = stats.GetTotalDeaths().ToString();

        Time.timeScale = 0.0f;
    }
}
