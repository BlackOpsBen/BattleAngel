using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFX : MonoBehaviour
{
    [SerializeField] private List<GameObject> hitPfx = new List<GameObject>();
    [SerializeField] private string hitSoundName;
    [SerializeField] private bool is3DSound = false;

    public void Play(Vector3 position, Quaternion rotation)
    {
        foreach (GameObject gameObj in hitPfx)
        {
            gameObj.transform.position = position;
            gameObj.transform.rotation = rotation;
            gameObj.GetComponent<ParticleSystem>().Play();
        }

        if (is3DSound)
        {
            AudioManager.Instance.PlaySound(hitSoundName, transform);
        }
        else
        {
            AudioManager.Instance.PlaySound(hitSoundName);
        }
    }    
}
