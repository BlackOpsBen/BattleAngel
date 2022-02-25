using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class ObjectiveDeathBehavior : MonoBehaviour, IDie
{
    [SerializeField] private SequenceExplosion[] sequenceExplosions;
    [SerializeField] private string fireSoundName;
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
        GetComponent<SpawnArea>().SetIsSpawning(false);
        
        RemoveFromMiniMap();

        SpawnManager.Instance.OnObjectiveDestroyed();

        StartCoroutine(PlaySequence());
    }

    private void RemoveFromMiniMap()
    {
        AppearOnMap appearOnMap;
        if (appearOnMap = GetComponent<AppearOnMap>())
        {
            MiniMap.Instance.RemoveItemFromMap(appearOnMap);
        }
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
                AudioManager.Instance.PlaySoundLoop(fireSoundName, transform, gameObject.name);
            }

            Shaker.ShakeAll(explosion.shakePreset);
        }

        UpdateObjective();
    }

    private static void UpdateObjective()
    {
        GameManager.Instance.numDestroyed++;
        ObjectiveListItem objective = GameManager.Instance.objectiveUI.GetObjective(GameManager.Instance.degeneratorObjectiveName);
        objective.InitializeObjective(GameManager.Instance.degeneratorObjectiveName, " ( " + GameManager.Instance.numDestroyed + " / " + GameManager.Instance.initialNumDegenerators + " )");
        if (GameManager.Instance.numDestroyed == GameManager.Instance.initialNumDegenerators)
        {
            objective.MarkComplete();
        }
    }

    [System.Serializable]
    private struct SequenceExplosion
    {
        public float delay;
        public GameObject[] itemsToEnable;
        public string explosionSound;
        public bool playFireSoundLoop;
        public ShakePreset shakePreset;
    }
}
