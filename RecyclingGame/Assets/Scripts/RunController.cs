using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RunController : MonoBehaviour
{
    public static RunController instance = null;
    public GameObject trashPrefab;

    public List<string> trashes_right = new List<string>();
    public List<string> trashes_wrong = new List<string>();
    private bool runPass;

    public PlayerController playerController;
    public GameObject player;
    public RunUI runUI;

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
        if (Input.GetKeyDown(KeyCode.Return) && SceneManager.GetActiveScene().name == "RunScene")
        {
            if (runUI.keyTutorial.activeSelf == true)
            {
                runUI.ShowKeyTutorial(false);
            }
            else if (runUI.trashDictionary.activeSelf == true)
            {
                runUI.ShowDictionary(false);
                StartCountDownDisplay(4);
                
            }
        }
    }

    void FindPlayer()
    {
        player = GameObject.Find("Player");
        playerController = player.GetComponent<PlayerController>();
    }
    void FindRunUI()
    {
        runUI = GameObject.Find("RunUIController").GetComponent<RunUI>();
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

    public void StartCountDownDisplay(int seconds)
    {
        StartCoroutine(CountDownDisplay(seconds));
    }

    IEnumerator CountDownDisplay(int seconds)
    {
        int count = seconds;
        while (count > 0)
        {
            if(count == 1) runUI.countDown.GetComponent<TextMeshProUGUI>().text = "START!";
            else runUI.countDown.GetComponent<TextMeshProUGUI>().text = (count-1).ToString();
            yield return new WaitForSeconds(1);
            count--;
        }

        StartRunning();
        runUI.countDown.SetActive(false);
        //StartCountDownDisappear(2);

    }

    public void StartCountDownDisappear(int seconds)
    {
        StartCoroutine(CountDownDisappear(seconds));
    }

    IEnumerator CountDownDisappear(int seconds)
    {
        int count = seconds;
        while (count > 0)
        {
            yield return new WaitForSeconds(1);
            count--;
        }
        runUI.countDown.SetActive(false);
    }


    void StartRunning()
    {
        if (playerController.state == PlayerState.Idle)
        {
            playerController.ChangeState(PlayerState.Running);
            playerController.animator.SetBool("run_bool", true);
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
        FindRunUI();
        FindPlayer();
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
