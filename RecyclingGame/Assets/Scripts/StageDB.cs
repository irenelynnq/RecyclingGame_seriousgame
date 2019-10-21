using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDB : MonoBehaviour
{
    //StageItem의 DB 관리
    public List<StageItem> stageList;
    public int stageCount = 1;
    public float starting = 10; //각각 다르면 DBInit 안으로 들어가야 함
    public float gap = 10; //각각 다르면 DBInit 안으로 들어가야 함

    public void DBInit()
    {
        for(int i = 0; i < stageCount; i++)
        {
            string name = "일반쓰레기";
            string preprocess = "포장하기";
            int criteria = 50;
            int trashCount = 19;
            int answerCount = 10;
            string trashPositionFileName = "TrashPosition_1";
            
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

            StageInit(newStage, trashPositionFileName);

            stageList.Add(newStage);
        }
    }

    public void StageInit(StageItem stage, string stageTrashPosition)
    {
        List<Dictionary<string, object>> data = CSVReader.Read(stageTrashPosition);

        for(var i = 0; i < stage.trashCount; i++)
        {
            Trash newTrash = new Trash();
            newTrash.id = (int)data[i]["id"];
            newTrash.name = (string)data[i]["name"];
            newTrash.is_answer = (bool)data[i]["is_answer"];
            newTrash.need_preprocess = (bool)data[i]["need_preprocess"];
            newTrash.sprite_name = (string)data[i]["sprite_name"];
            newTrash.xPosition = (float)data[i]["xPosition"];
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

        }
    }
}
