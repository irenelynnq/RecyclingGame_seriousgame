using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameState currentGameState = GameState.menu;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    
    public StageDB db;

    public GameObject run;
    public GameObject player;

    public RunController runController;
    public PlayerController playerController;

    public List<Trash> trashes_right = new List<Trash>();
    public List<Trash> trashes_wrong = new List<Trash>();

    // Start is called before the first frame update
    void Start()
    {
        db = new StageDB();
        db.DBInit();
        run = GameObject.Find("RunController");
        player = GameObject.Find("Player");
        runController = run.GetComponent<RunController>();
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //making stage
            MakeRunStage(1);
        }
    }

    public void StartGame()
    {

    }
    public void GameOver()
    {

    }
    public void BackToMenu()
    {

    }
    public void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
    }


    public void MakeRunStage(int level)
    {
        runController.InitRunStage(level, db.GetStageItem(level));
        //db.StageInit(db.GetStageItem(level), "TrashPosition_" + level.ToString());

        playerController.PlayerClean();
    }

    public void GetRunResult(List<Trash> right, List<Trash> wrong)
    {
        if (trashes_right.Count != 0) trashes_right.RemoveRange(0, trashes_right.Count);
        if (trashes_wrong.Count != 0) trashes_wrong.RemoveRange(0, trashes_wrong.Count);
        int i;
        for(i = 0; i < right.Count; i++)
        {
            trashes_right.Add(right[i]);
        }
        for(i = 0; i < wrong.Count; i++)
        {
            trashes_wrong.Add(wrong[i]);
        }
    }
}
