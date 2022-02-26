using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    private string objeciveName = "???";

    private void Start()
    {
        GameManager.Instance.objectiveUI.NewObjective(objeciveName, "");
    }

    public void Defeat()
    {
        GameManager.Instance.objectiveUI.GetObjective(objeciveName).MarkComplete();

        // TODO end game
    }
}
