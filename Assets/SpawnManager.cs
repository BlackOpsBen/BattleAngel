using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance { get; private set; }

    private SpawnArea[] spawnAreas;

    private int objectivesDestroyed = 0;

    [Header("Basic Enemy Spawning")]
    [SerializeField] private GameObject basicEnemy;
    [SerializeField] private float baseSpawnTime = 10.0f;
    [SerializeField] private float timeDecreasePerObjective = 2.0f;
    private float spawnTimer = float.MaxValue;

    [Header("Mini Boss Spawning")]
    [SerializeField] private GameObject miniBoss;

    [Header("Final Boss Spawning")]
    [SerializeField] private GameObject finalBoss;
    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        spawnAreas = FindObjectsOfType<SpawnArea>();
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > baseSpawnTime)
        {
            SpawnBasicEnemy();
            spawnTimer = 0.0f;
        }
    }

    public void OnObjectiveDestroyed()
    {
        objectivesDestroyed++;

        baseSpawnTime -= timeDecreasePerObjective;

        if (IsObjectiveRemaining())
        {
            SpawnMiniBoss();
        }
        else
        {
            SpawnFinalBoss();
        }
    }

    public void SpawnBasicEnemy()
    {
        Spawn(basicEnemy);
    }

    public void SpawnMiniBoss()
    {
        Spawn(miniBoss);
    }

    public void SpawnFinalBoss()
    {
        GameObject boss = Instantiate(finalBoss, spawnPoint.position, Quaternion.identity);

        boss.AddComponent<FinalBoss>();
    }

    private void Spawn(GameObject enemyType)
    {
        SpawnArea spawn = GetRandomActiveSpawnArea();

        if (spawn != null)
        {
            spawn.Spawn(enemyType);
        }
    }

    private SpawnArea GetRandomActiveSpawnArea()
    {
        List<SpawnArea> activeSpawns = new List<SpawnArea>();

        foreach (SpawnArea spawnArea in spawnAreas)
        {
            if (spawnArea.GetIsSpawning())
            {
                activeSpawns.Add(spawnArea);
            }
        }

        if (activeSpawns.Count > 0)
        {
            int rand = UnityEngine.Random.Range(0, activeSpawns.Count);

            return activeSpawns[rand];
        }
        else
        {
            return null;
        }
    }

    private bool IsObjectiveRemaining()
    {
        return objectivesDestroyed < spawnAreas.Length;
    }
}
