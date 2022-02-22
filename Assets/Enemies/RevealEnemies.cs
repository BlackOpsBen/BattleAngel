using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealEnemies : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        RevealedByLight reveal;
        if (reveal = other.gameObject.GetComponent<RevealedByLight>())
        {
            reveal.SetVisibility(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RevealedByLight reveal;
        if (reveal = other.gameObject.GetComponent<RevealedByLight>())
        {
            reveal.SetVisibility(false);
        }
    }
}
