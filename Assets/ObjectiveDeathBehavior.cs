using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveDeathBehavior : MonoBehaviour, IDie
{
    [SerializeField] private SequenceExplosion[] sequenceExplosions;
    [SerializeField] private string fireSoundName;
    [SerializeField] private string uniqueLoopID;
    [SerializeField] private AudioSource soundToStop;
    [SerializeField] private Transform targetToReTag;

    private void Start()
    {
        foreach (SequenceExplosion explosion in sequenceExplosions)
        {
            foreach (GameObject gObj in explosion.itemsToEnable)
            {
                gObj.SetActive(false);
            }
        }
    }

    public void Die()
    {
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        soundToStop.Stop();
        targetToReTag.tag = "Untagged";

        foreach (SequenceExplosion explosion in sequenceExplosions)
        {
            yield return new WaitForSeconds(explosion.delay);

            foreach (GameObject gObj in explosion.itemsToEnable)
            {
                gObj.SetActive(true);
            }

            AudioManager.Instance.PlaySound(explosion.explosionSound, explosion.itemsToEnable[0].transform);

            if (explosion.playFireSoundLoop)
            {
                AudioManager.Instance.PlaySoundLoop(fireSoundName, transform, uniqueLoopID);
            }
        }
    }

    [System.Serializable]
    private struct SequenceExplosion
    {
        public float delay;
        public GameObject[] itemsToEnable;
        public string explosionSound;
        public bool playFireSoundLoop;
    }
}