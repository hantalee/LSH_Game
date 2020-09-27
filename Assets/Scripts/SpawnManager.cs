﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public delegate void SpawnEventHandler(StageData data);
    public static event SpawnEventHandler OnStartSpawn;

    public List<StageData> stages;
    public StageData currentRound;
    public int currRound;
    public float roundDelay = 10.0f;
    public bool goNextRound = false;
    private int spawnOver;

    private static SpawnManager instance;
    public static SpawnManager Instance
    {
        get
        {
            if (instance != null)
                return instance;
            return null;
        }
    }


    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void Start()
    {
        stages = DataManager.Instance.StageDatas;
        currRound = 0;
        spawnOver = 0;
        goNextRound = true;
    }

    private void Update()
    {
        if(goNextRound)
        {
            goNextRound = false;
            currRound += 1;
            currentRound = FindRound(currRound);
            if(currentRound == null)
            {
                //스테이지가 끝났음
                Debug.Log("모든 라운드가 끝났습니다");
                goNextRound = false;
            }
            else if(currentRound.Type == RoundType.Store)
            {
                //상점 UI호출
                Debug.Log("상점 호출");
                goNextRound = false;
            }
            else
            {
                StartSpawn(currentRound);
                spawnOver = 0;
            }

        }
    }

    StageData FindRound(int round)
    {
        foreach (StageData stage in stages)
        {
            int r = stage.Round;
            if(r == round)
            {
                return stage;
            }
        }
        Debug.Log("해당 라운드를 찾지 못했습니다 : " + round);
        return null;
    }

    void StartSpawn(StageData data)
    {
        OnStartSpawn(data);
    }

    public void CheckSpawnOver()
    {
        spawnOver += 1;
        if (spawnOver == 2)
            Invoke("GoNextRound", roundDelay);
    }

    void GoNextRound()
    {
        Debug.Log("GoNextRound");
        goNextRound = true;
    }
}
