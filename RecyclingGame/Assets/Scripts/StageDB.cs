using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDB : MonoBehaviour
{
    //StageItem의 DB 관리
    public List<StageItem> stageList;
    public int stageCount;
    public float starting; //각각 다르면 DBInit 안으로 들어가야 함
    public float gap; //각각 다르면 DBInit 안으로 들어가야 함

    public void DBInit()
    {
        for(int i = 1; i <= stageCount; i++)
        {
            string name = "example";
            int criteria = 50;
            int trashCount = 3;
            int answerCount = 2;
            
            StageItem newStage = new StageItem();
            newStage.stage_number = i;
            newStage.name = name;
            newStage.pass_criteria = criteria;
            newStage.is_passed = false;
            newStage.trashCount = trashCount;
            newStage.answerCount = answerCount;
            newStage.trashStartingPoint = starting;
            newStage.trashGap = gap;
            StageInit(newStage);

            stageList.Add(newStage);
        }
    }

    public void StageInit(StageItem stage)
    {

    }
}
