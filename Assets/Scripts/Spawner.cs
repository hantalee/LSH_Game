using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    SpawnData[] currSpawnData;
    public float spawnDelay = 1.0f;
    private float currentTime;
    public bool spawning;
    public int waveCount;
    public int monsterCount;

    private void Start()
    {
        currentTime = spawnDelay;
        StageManager.OnStartSpawn += StartWave;
    }

    private void Update()
    {
        if(spawning)
        {
            currentTime -= Time.deltaTime;
            if(currentTime <= 0)
            {
                currentTime = spawnDelay;
                StartSpawn();
                waveCount++;

                if (currSpawnData.Length == waveCount)
                    spawning = false;
            }
        }
    }

    void StartWave(StageData data)
    {
        spawning = true;
        waveCount = 0;
        currSpawnData = data.SpawnData;
        monsterCount = 0;
    }

    void StartSpawn()
    {
        string name = currSpawnData[waveCount].monsterName;
        int monsterNum = currSpawnData[waveCount].monsterNum;
        for (int i = 0; i < monsterNum; ++i)
        {
            SpawnMonster(name);
        }
    }

    void SpawnMonster(string name)
    {
        BaseMonster monster = ObjectPooling.Instance.GetMonsterByName(name);
        monster.transform.position = transform.position;
        monster.spawner = this;
        monsterCount++;
    }
}
