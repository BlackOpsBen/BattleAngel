using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFX : MonoBehaviour
{
    [SerializeField] private List<GameObject> hitPfx = new List<GameObject>();
    [SerializeField] private string hitSoundName;

    public void Play(Vector3 position, Quaternion rotation)
    {
        foreach (GameObject gameObj in hitPfx)
        {
            gameObj.transform.position = position;
            gameObj.transform.rotation = rotation;
            gameObj.GetComponent<ParticleSystem>().Play();
        }

        AudioManager.Instance.PlaySound(hitSoundName);
    }    
}
