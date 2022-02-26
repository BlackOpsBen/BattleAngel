using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashUIManager : MonoBehaviour
{
    [SerializeField] private GameObject splashUI;
    [SerializeField] private GameObject instructionsUI;

    private void Start()
    {
        instructionsUI.SetActive(false);
    }

    public void OnPlay()
    {
        SceneManager.LoadScene(1);
    }

    public void OnShowInstructions()
    {
        splashUI.SetActive(false);
        instructionsUI.SetActive(true);
    }

    public void OnBack()
    {
        instructionsUI.SetActive(false);
        splashUI.SetActive(true);
    }
}
