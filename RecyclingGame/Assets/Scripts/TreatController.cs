using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatController : MonoBehaviour
{
    public GameObject trashPrefab;
    public Dictionary<int, Vector3> treatPosition;
    // Start is called before the first frame update
    void Start()
    {
        InitDict();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitTreatStage(int level, StageItem stage)
    {
        List<Dictionary<string, object>> data = CSVReader.Read("FileResources/" + "TreatTrash_" + level.ToString());

        //StageItem으로 stage initialize
        for (int i = 0; i < stage.trashCount; i++)
        {
            GameObject newTrash = Instantiate(trashPrefab);
            Trash newTrashInfo = newTrash.GetComponent<Trash>();

            newTrashInfo.id = (string)data[i]["id"];
            newTrashInfo.name = (string)data[i]["name"];
            newTrashInfo.is_answer = true;
            newTrashInfo.need_preprocess = (int)data[i]["need_preprocess"] == 0 ? false : true;
            newTrashInfo.sprite_name = (string)data[i]["sprite_name"];

            int position_number = (int)data[i]["position_number"];

            stage.treatTrashDict.Add(position_number, newTrashInfo);

            Debug.Log("treat trash " + newTrashInfo.name);

            Sprite trashSprite = Resources.Load<Sprite>("Art/Trash/" + newTrashInfo.sprite_name);
            SpriteRenderer sr = newTrash.GetComponent<SpriteRenderer>();
            sr.sprite = trashSprite;
            newTrash.transform.position = treatPosition[position_number];

            Debug.Log("Init treat Trash " + newTrashInfo.name);
        }

        //각 지점마다 클리어 표시할 게 생기게 한 다음 모두 disable해야 함

    }

    void InitDict()
    {
        treatPosition.Add(1, new Vector3(0, 0));
        treatPosition.Add(2, new Vector3(0, 0));
        treatPosition.Add(3, new Vector3(0, 0));
        treatPosition.Add(4, new Vector3(0, 0));
        treatPosition.Add(5, new Vector3(0, 0));
        treatPosition.Add(6, new Vector3(0, 0));
        treatPosition.Add(7, new Vector3(0, 0));
        treatPosition.Add(8, new Vector3(0, 0));
        treatPosition.Add(9, new Vector3(0, 0));
        treatPosition.Add(10, new Vector3(0, 0));
        Debug.Log("finished initdict");
    }

    void GotAnswer(int i)
    {
        //i 위치에 있는 쓰레기 정답 표시...
    }
}
