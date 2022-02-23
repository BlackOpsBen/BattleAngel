using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealEnemies : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        RevealByLightDelegate reveal;
        if (reveal = other.gameObject.GetComponent<RevealByLightDelegate>())
        {
            Debug.Log("Revealing " + other.name);
            reveal.Reveal();
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        RevealedByLight reveal;
        if (reveal = other.gameObject.GetComponent<RevealedByLight>())
        {
            reveal.SetVisibility(true);
        }
    }*/

    /*private void OnTriggerExit(Collider other)
    {
        RevealedByLight reveal;
        if (reveal = other.gameObject.GetComponent<RevealedByLight>())
        {
            reveal.SetVisibility(false);
        }
    }*/
}
