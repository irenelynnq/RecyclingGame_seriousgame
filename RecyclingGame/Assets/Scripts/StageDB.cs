using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDB
{
    //StageItem의 DB 관리
    public List<StageItem> stageList = new List<StageItem>();
    public int stageCount = 5;
    public float starting = 20; //각각 다르면 DBInit 안으로 들어가야 함
    public float gap = 21; //각각 다르면 DBInit 안으로 들어가야 함


    public StageItem GetStageItem(int i)
    {
        //((i < stageCount) && (i > 0))
        
        return stageList[i-1];
        
        
    }
    public void DBInit()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("FileResources/" + "StageList");

        for (int i = 0; i < stageCount; i++)
        {
            Debug.Log("start inserting stage " + (i + 1).ToString() + "in DB");
            //string trashPositionFileName = "TrashPosition_1";
            
            StageItem newStage = new StageItem();
            newStage.stage_number = i+1;
            newStage.name = (string)data[i]["name"];
            newStage.preprocess = (string)data[i]["preprocess"];
            newStage.is_passed = false;
            newStage.trashCount = (int)data[i]["trashCount"];
            newStage.answerCount = (int)data[i]["answerCount_1"];
            newStage.pass_criteria = (int)data[i]["pass_criteria"];
            newStage.treatTrashCount = 12;
            newStage.treatAnswerCount = (int)data[i]["answerCount_2"];
            newStage.trashStartingPoint = starting;
            newStage.trashGap = gap;
            stageList.Add(newStage);
            Debug.Log("finished inserting DB stage " + (i+1).ToString());
        }
    }

    /*
    public void StageInit(StageItem stage, string stageTrashPosition)
    {
        List<Dictionary<string, object>> data = CSVReader.Read("FileResources/"+stageTrashPosition);


    }
    */
}
