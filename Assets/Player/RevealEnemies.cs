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
            reveal.Reveal();
        }
    }
}
