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
            StartCoroutine(BossSequence());
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

    private IEnumerator BossSequence()
    {
        PlayMusic playMusic = AudioManager.Instance.GetComponent<PlayMusic>();

        // lower music volume
        playMusic.LowerVolume();

        yield return new WaitForSeconds(0.5f);

        // Player dialog "That's it!"
        AudioManager.Instance.PlayDialog(AudioManager.PLAYERNAME, "thats-the-last-one", INTERRUPT_MODE: AudioManager.INTERRUPT_SELF);

        yield return new WaitForSeconds(3.0f);

        // Weird noise
        AudioManager.Instance.PlaySound("strange_noise", "SFX");

        yield return new WaitForSeconds(2.0f);

        // Player dialog "what was that?"
        AudioManager.Instance.PlayDialog(AudioManager.PLAYERNAME, "what-was-that", INTERRUPT_MODE: AudioManager.INTERRUPT_SELF);

        yield return new WaitForSeconds(1.0f);

        // Dialog "whatever it is, kill it!
        AudioManager.Instance.PlayDialog(AudioManager.SUPPORTNAME, "kill_it", INTERRUPT_MODE: AudioManager.INTERRUPT_SELF);

        yield return new WaitForSeconds(3.0f);

        playMusic.RestoreVolume();
        // Start boss music
        playMusic.StartBossMusic();
        SpawnFinalBoss();
    }
}
