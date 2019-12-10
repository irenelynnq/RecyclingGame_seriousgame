using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class RunController : MonoBehaviour
{
    public static RunController instance = null;
    public GameObject trashPrefab;

    public List<int> trashes_right = new List<int>();
    public List<int> trashes_wrong = new List<int>();
    private bool runPass;

    public PlayerController playerController;
    public GameObject player;
    public RunUI runUI;

    public Camera mainCam;

    private bool canMenu;
   

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
        //mainCam.transform.position = new Vector3(0, 0, 0);
        runPass = false;
        canMenu = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "RunScene")
        {
            if (GameManager.instance.currentGameState == GameState.menu)
            {
                if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    runUI.MoveMenuPointer();
                }
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    GameManager.instance.SetGameState(GameState.inGame);
                    runUI.OutMenu();
                    SoundManager.instance.FxSound(SoundManager.instance.next_fx);
                    if (runUI.menuState == MenuState.cont)
                    {
                        if(!(runUI.keyTutorial.activeSelf) && !(runUI.trashDictionary.activeSelf))
                        {
                            runUI.countDown.SetActive(true);
                            StartCountDownDisplay(4);
                        }
                    }
                    else if(runUI.menuState == MenuState.mainTitle)
                    {
                        SoundManager.instance.AudioStop(SoundManager.instance.bgmSource);
                        GameManager.instance.is_first_run = true;
                        GameManager.instance.is_first_treat = true;
                        for (int i = 1; i <= GameManager.instance.currentLevel; i++)
                        {
                            GameManager.instance.db.GetStageItem(i).StageItemClean();
                        }
                        SceneManager.LoadScene("TitleScene");
                    }
                }
            }

            else if(GameManager.instance.currentGameState == GameState.inGame)
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (GameManager.instance.is_first_run == true)
                    {
                        if (runUI.keyTutorial.activeSelf == true)
                        {
                            runUI.ShowDictionary(true);
                            runUI.ShowKeyTutorial(false);
                            GameManager.instance.is_first_run = false;
                        }
                        else if (runUI.trashDictionary.activeSelf == true)
                        {
                            runUI.ShowDictionary(false);
                            runUI.countDown.SetActive(true);
                            SoundManager.instance.AudioVolume(SoundManager.instance.bgmSource, 1f);
                            StartCountDownDisplay(4);
                        }
                    }
                    else if (runUI.trashDictionary.activeSelf == true)
                    {
                        runUI.ShowDictionary(false);
                        runUI.countDown.SetActive(true);
                        SoundManager.instance.AudioVolume(SoundManager.instance.bgmSource, 1f);
                        StartCountDownDisplay(4);
                    }
                }

                else if (Input.GetKeyDown(KeyCode.Escape) && canMenu)
                {
                    playerController.ChangeState(PlayerState.Idle);
                    playerController.animator.SetBool("run_bool", false);
                    runUI.ShowMenu();
                    GameManager.instance.SetGameState(GameState.menu);
                }
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

    public void GetRunResult(List<int> right, List<int> wrong)
    {
        int i;
        for (i = 0; i < right.Count; i++)
        {
            trashes_right.Add(right[i]);
        }
        for (i = 0; i < wrong.Count; i++)
        {
            trashes_wrong.Add(wrong[i]);
        }

    }

    public void StartCountDownDisplay(int seconds)
    {
        StartCoroutine(CountDownDisplay(seconds));
    }

    IEnumerator CountDownDisplay(int seconds)
    {
        canMenu = false;
        int count = seconds;
        while (count > 0)
        {
            if (count == 1)
            {
                runUI.countDown.GetComponent<TextMeshProUGUI>().text = "START!";
                SoundManager.instance.FxSound(SoundManager.instance.start_fx);
            }
            else runUI.countDown.GetComponent<TextMeshProUGUI>().text = (count - 1).ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        runUI.countDown.SetActive(false);
        StartRunning();
        canMenu = true;
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
        trashes_right = new List<int>();
        trashes_wrong = new List<int>();
        List<Dictionary<string, object>> data = CSVReader.Read("FileResources/" + "TrashPosition_" + level.ToString());
        canMenu = true;

        //StageItem으로 stage initialize
        for (int i = 0; i < stage.trashCount; i++)
        {
            GameObject newTrash = Instantiate(trashPrefab);
            Trash newTrashInfo = newTrash.GetComponent<Trash>();

            newTrashInfo.id = (int)data[i]["id"];
            newTrashInfo.name = (string)data[i]["name"];
            newTrashInfo.is_answer = (int)data[i]["is_answer"] == 0 ? false : true;
            newTrashInfo.sprite_name = (string)data[i]["sprite_name"];
            if (newTrashInfo.is_answer)
            {
                stage.answerSpriteNameDict.Add(newTrashInfo.id, newTrashInfo.sprite_name);
            }
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
            if (trashSprite == null)
            {
                if (newTrashInfo.is_answer) trashSprite = Resources.Load<Sprite>("Art/Trash/" + "Zright");
                else trashSprite = Resources.Load<Sprite>("Art/Trash/" + "Zwrong");
            }
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
