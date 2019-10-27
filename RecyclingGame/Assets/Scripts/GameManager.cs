using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    menu,
    inGame,
    gameOver
}

public enum GameScene
{
    run,
    runresult,
    loading,
    treat,
    treatresult
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameState currentGameState = GameState.menu;
    public GameScene currentGameScene = GameScene.run;
    public int currentLevel;

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
        currentLevel = 1;

        MakeRunStage();
    }

    void Update()
    {
        
        
    }

    /*
    public void StartGame()
    {

    }
    public void GameOver()
    {

    }
    public void BackToMenu()
    {

    }
    */
    public void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
    }


    public void MakeRunStage()
    {
        runController.InitRunStage(currentLevel, db.GetStageItem(currentLevel));

        //playerController.PlayerClean();
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

    public void TurnToTreatStage()
    {
        SceneManager.LoadScene("TreatScene");
        //MakeTreatStage();
    }

    public void MakeTreatStage()
    {
        TreatController.instance.InitTreatStage(currentLevel, db.GetStageItem(currentLevel));
    }

    public void LevelUp()
    {
        currentLevel += 1;
        SceneManager.LoadScene("RunScene");
        MakeRunStage();
    }
}
