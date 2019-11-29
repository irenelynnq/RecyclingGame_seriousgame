using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TreatController : MonoBehaviour
{
    public static TreatController instance = null;

    public GameObject trashPrefab;
    public GameObject tempCoinPrefab;

    public Dictionary<int, Vector3> treatPosition;
    public StageItem currentStage;
    public int playerAnswerCount;
    public List<string> playerAnswerSpriteNames;

    Sprite doneTrashSprite; 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        treatPosition = new Dictionary<int, Vector3>();
        InitDict();
    }
    // Start is called before the first frame update
    void Start()
    {
        doneTrashSprite = Resources.Load<Sprite>("Art/Trash/Trash_Wrap");
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void InitTreatStage(int level, StageItem stage)
    {
        List<Dictionary<string, object>> data = CSVReader.Read("FileResources/" + "TreatTrash_" + level.ToString());
        currentStage = stage;
        playerAnswerCount = 0;

        //StageItem으로 stage initialize
        for (int i = 0; i < stage.treatTrashCount; i++)
        {
            GameObject newTrash = Instantiate(trashPrefab);
            Trash newTrashInfo = newTrash.GetComponent<Trash>();

            newTrashInfo.id = (int)data[i]["id"];
            newTrashInfo.name = (string)data[i]["name"];
            newTrashInfo.is_answer = true;
            newTrashInfo.need_preprocess = (int)data[i]["need_preprocess"] == 0 ? false : true;
            newTrashInfo.sprite_name = (string)data[i]["sprite_name"];
            newTrashInfo.hitcount = 0;
            newTrashInfo.treatDone = false;

            int position_number = (int)data[i]["position_number"];

            currentStage.treatTrashDict.Add(position_number, newTrashInfo);

            Debug.Log("treat trash " + newTrashInfo.name);

            Sprite trashSprite = Resources.Load<Sprite>("Art/Trash/" + newTrashInfo.sprite_name);
            if (trashSprite == null)
            {
                if (newTrashInfo.need_preprocess) trashSprite = Resources.Load<Sprite>("Art/Trash/" + "Zright");
                else trashSprite = Resources.Load<Sprite>("Art/Trash/" + "Zwrong");
            }
            SpriteRenderer sr = newTrash.GetComponent<SpriteRenderer>();
            sr.sprite = trashSprite;
            newTrash.transform.position = treatPosition[position_number];

            Debug.Log("Init treat Trash " + newTrashInfo.name);
        }

    }

    void InitDict()
    {
        treatPosition.Add(1, new Vector3(-6.32f, 1.28f));
        treatPosition.Add(2, new Vector3(-2.11f, 1.28f));
        treatPosition.Add(3, new Vector3(2.11f, 1.28f));
        treatPosition.Add(4, new Vector3(6.32f, 1.28f));

        treatPosition.Add(5, new Vector3(-6.32f, -0.88f));
        treatPosition.Add(6, new Vector3(-2.11f, -0.88f));
        treatPosition.Add(7, new Vector3(2.11f, -0.88f));
        treatPosition.Add(8, new Vector3(6.32f, -0.88f));

        treatPosition.Add(9, new Vector3(-6.32f, -3.04f));
        treatPosition.Add(10, new Vector3(-2.11f, -3.04f));
        treatPosition.Add(11, new Vector3(2.11f, -3.04f));
        treatPosition.Add(12, new Vector3(6.32f, -3.04f));
        Debug.Log("finished initdict");
    }


    public void GotAnswer(int i)
    {
        //i 위치에 있는 쓰레기 정답 표시...

        currentStage.treatTrashDict[i].GetComponentInParent<SpriteRenderer>().sprite = doneTrashSprite;
        playerAnswerCount++;
        playerAnswerSpriteNames.Add(currentStage.treatTrashDict[i].sprite_name);
        Debug.Log("Answer!");
        if (playerAnswerCount == currentStage.treatAnswerCount) AllAnswerClear();
        
    }

    public void AllAnswerClear()
    {
        Debug.Log("Finish!");

    }

    public Trash GetTrashAtPosition(int i)
    {
        return currentStage.treatTrashDict[i];
    }
}
