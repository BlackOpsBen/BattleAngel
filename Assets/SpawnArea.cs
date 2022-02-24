using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    private int nextToUse;
    private bool isSpawning = true;

    private void Awake()
    {
        nextToUse = UnityEngine.Random.Range(0, spawnPoints.Length);
    }

    public void Spawn(GameObject enemyPrefab)
    {
        Instantiate(enemyPrefab, spawnPoints[nextToUse].position, Quaternion.identity);

        SetNextToUse();
    }

    private void SetNextToUse()
    {
        nextToUse = spawnPoints.Length % (nextToUse + 1);
    }

    public void SetIsSpawning(bool value)
    {
        isSpawning = value;
    }

    public bool GetIsSpawning()
    {
        return isSpawning;
    }
}
