using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevealByLightDelegate : MonoBehaviour
{
    [SerializeField] private RevealedByLight revealedByLight;

    public void Reveal()
    {
        revealedByLight.Reveal();
    }
}
