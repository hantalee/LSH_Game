using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public delegate void SpawnEventHandler(StageData data);
    public static event SpawnEventHandler OnStartSpawn;

    public List<StageData> stages;

    public StageData currRound;
    public int roundNum;

    public Spawner[] spawners;
    public SkillGiver giver;

    private void Start()
    {
        currRound = null;
        stages = DataManager.Instance.StageDatas;
        roundNum = 0;
    }

    public void OnClickStageButton()
    {
        for(int i = 0; i < spawners.Length; ++i)
        {
            if ((spawners[i].monsterCount > 0) || (spawners[i].spawning == true))
            {
                Debug.Log("모든 몬스터를 죽여야 다음 스테이지로 넘어갑니다.");
                return;
            }
        }

        roundNum += 1;
        currRound = FindRound(roundNum);
        if (currRound == null) return; // 스테이지 종료

        UIEventHandler.ChangeRound(roundNum);
        if(currRound.Type == RoundType.SkillGiver)
        {
            giver.ActivateSkillGiver();
        }
        else
        {
            StartSpawn(currRound);
        }
    }

    StageData FindRound(int num)
    {
        foreach (StageData stage in stages)
        {
            int r = stage.Round;
            if (r == num)
                return stage;
        }
        Debug.Log("해당 라운드를 찾지 못했습니다 : " + num);
        return null;
    }

    void StartSpawn(StageData data)
    {
        OnStartSpawn(data);
    }
}
