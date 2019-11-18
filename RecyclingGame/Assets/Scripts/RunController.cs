using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunController : MonoBehaviour
{
    public static RunController instance = null;
    public GameObject trashPrefab;

    public List<string> trashes_right = new List<string>();
    public List<string> trashes_wrong = new List<string>();
    private bool runPass;

    public Camera mainCam;
   

    //Run Map의 전반적인 게임을 관리합니다.
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
    }
    // Start is called before the first frame update
    void Start()
    {
        mainCam.transform.position = new Vector3(0, 0, 0);
        runPass = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetRunResult(List<Trash> right, List<Trash> wrong)
    {
        int i;
        for (i = 0; i < right.Count; i++)
        {
            trashes_right.Add(right[i].name);
        }
        for (i = 0; i < wrong.Count; i++)
        {
            trashes_wrong.Add(wrong[i].name);
        }

    }

    public void PassRunStage()
    {
        runPass = true;
    }

    public bool DidPassRunStage()
    {
        return runPass;
    }

    public void InitRunStage(int level, StageItem stage)
    {
        runPass = false;
        trashes_right = new List<string>();
        trashes_wrong = new List<string>();
        List<Dictionary<string, object>> data = CSVReader.Read("FileResources/" + "TrashPosition_" + level.ToString());

        //StageItem으로 stage initialize
        for (int i = 0; i < stage.trashCount; i++)
        {
            GameObject newTrash = Instantiate(trashPrefab);
            Trash newTrashInfo = newTrash.GetComponent<Trash>();

            newTrashInfo.id = ((int)data[i]["id"]).ToString();
            newTrashInfo.name = (string)data[i]["name"];
            newTrashInfo.is_answer = (int)data[i]["is_answer"] == 0 ? false : true;
            newTrashInfo.need_preprocess = (int)data[i]["need_preprocess"] == 0 ? false : true;
            newTrashInfo.sprite_name = (string)data[i]["sprite_name"];
            newTrashInfo.xPosition = stage.trashStartingPoint + (float)((int)data[i]["xPosition"] * stage.trashGap);
            switch ((string)data[i]["yPosition"])
            {
                case "Up":
                    newTrashInfo.yPosition = TrashPosition.Up;
                    break;
                case "Middle":
                    newTrashInfo.yPosition = TrashPosition.Middle;
                    break;
                case "Down":
                    newTrashInfo.yPosition = TrashPosition.Down;
                    break;
                default:
                    Debug.Log("Error reading yPosition");
                    break;
            }

            stage.trashList.Add(newTrashInfo);
            if (newTrashInfo.is_answer) stage.answerTrashList.Add(newTrashInfo);
            Debug.Log("DB trash " + newTrashInfo.name);

            Sprite trashSprite = Resources.Load<Sprite>("Art/Trash/"+newTrashInfo.sprite_name);
            SpriteRenderer sr = newTrash.GetComponent<SpriteRenderer>();
            sr.sprite = trashSprite;
            float y = 0f;
            switch (newTrashInfo.yPosition)
            {
                case TrashPosition.Up:
                    y = 2.08f;
                    break;
                case TrashPosition.Middle:
                    y = 0.32f;
                    break;
                case TrashPosition.Down:
                    y = -1.44f;
                    break;
            }
            newTrash.transform.position = new Vector3(newTrashInfo.xPosition, y);

            Debug.Log("Init Trash " + newTrashInfo.name);
        }

        

    }
   
}
