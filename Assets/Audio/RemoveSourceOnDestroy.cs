using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveSourceOnDestroy : MonoBehaviour
{
    private void OnDestroy()
    {
        AudioManager.Instance.RemoveDestroyed3DSound(GetComponent<AudioSource>());
    }
}
