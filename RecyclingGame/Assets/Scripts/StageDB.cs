using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDB
{
    //StageItem의 DB 관리
    public List<StageItem> stageList = new List<StageItem>();
    public int stageCount = 1;
    public float starting = 20; //각각 다르면 DBInit 안으로 들어가야 함
    public float gap = 21; //각각 다르면 DBInit 안으로 들어가야 함


    public StageItem GetStageItem(int i)
    {
        //((i < stageCount) && (i > 0))
        
        return stageList[i-1];
        
        
    }
    public void DBInit()
    {
        for(int i = 0; i < stageCount; i++)
        {
            Debug.Log("start inserting stage " + (i + 1).ToString() + "in DB");
            string name = "일반쓰레기";
            string preprocess = "포장하기";
            int criteria = 2;
            int trashCount = 20;
            int answerCount = 6;
            //string trashPositionFileName = "TrashPosition_1";
            
            StageItem newStage = new StageItem();
            newStage.stage_number = i+1;
            newStage.name = name;
            newStage.preprocess = preprocess;
            newStage.pass_criteria = criteria;
            newStage.is_passed = false;
            newStage.trashCount = trashCount;
            newStage.answerCount = answerCount;
            newStage.treatTrashCount = 12;
            newStage.treatAnswerCount = 6;
            newStage.trashStartingPoint = starting;
            newStage.trashGap = gap;
            Debug.Log("stage 1 setting");
            stageList.Add(newStage);
            Debug.Log("finished inserting DB stage " + (i+1).ToString());
        }
    }

    public void StageInit(StageItem stage, string stageTrashPosition)
    {
        List<Dictionary<string, object>> data = CSVReader.Read("FileResources/"+stageTrashPosition);


    }
}
