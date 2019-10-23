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
            int criteria = 50;
            int trashCount = 19;
            int answerCount = 10;
            //string trashPositionFileName = "TrashPosition_1";
            
            StageItem newStage = new StageItem();
            newStage.stage_number = i+1;
            newStage.name = name;
            newStage.preprocess = preprocess;
            newStage.pass_criteria = criteria;
            newStage.is_passed = false;
            newStage.trashCount = trashCount;
            newStage.answerCount = answerCount;
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

        /*
        for(var i = 0; i < stage.trashCount; i++)
        {
            
            Trash newTrash;
            newTrash.id = (int)data[i]["id"];
            newTrash.name = (string)data[i]["name"];
            newTrash.is_answer = (int)data[i]["is_answer"] == 0 ? false : true;
            newTrash.need_preprocess = (int)data[i]["need_preprocess"] == 0 ? false : true;
            newTrash.sprite_name = (string)data[i]["sprite_name"];
            newTrash.xPosition = stage.trashStartingPoint + ((float)data[i]["xPosition"] * stage.trashGap);
            switch ((string)data[i]["yPosition"])
            {
                case "Up":
                    newTrash.yPosition = TrashPosition.Up;
                    break;
                case "Middle":
                    newTrash.yPosition = TrashPosition.Middle;
                    break;
                case "Down":
                    newTrash.yPosition = TrashPosition.Down;
                    break;
                default:
                    Debug.Log("Error reading yPosition");
                    break;
            }

            stage.trashList.Add(newTrash);
            if (newTrash.is_answer) stage.answerTrashList.Add(newTrash);
            Debug.Log("DB trash " + newTrash.name);

        }
        */
    }
}
