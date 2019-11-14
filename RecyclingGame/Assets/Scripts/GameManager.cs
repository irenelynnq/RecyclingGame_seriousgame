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
    public int life;
    public int score;

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

    public RunController runController;
    public PlayerController playerController;

    public List<string> trashes_right = new List<string>();
    public List<string> trashes_wrong = new List<string>();

    public int treatedCount;

    // Start is called before the first frame update
    void Start()
    {
        db = new StageDB();
        db.DBInit();
        run = GameObject.Find("RunController");
        runController = run.GetComponent<RunController>();

        StartGame();

        
    }

    void Update()
    {
        
        
    }

    
    public void StartGame()
    {
        currentLevel = 1;
        life = 3;
        score = 0;
        MakeRunStage();
    }
    
    public void BackToMenu()
    {

    }
    

    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void SetGameState(GameState newGameState)
    {
        currentGameState = newGameState;
    }


    public void MakeRunStage()
    {
        SceneManager.LoadScene("RunScene");
    }

    public void GetRunResult(List<Trash> right, List<Trash> wrong)
    {
        if (trashes_right.Count != 0) trashes_right.RemoveRange(0, trashes_right.Count);
        if (trashes_wrong.Count != 0) trashes_wrong.RemoveRange(0, trashes_wrong.Count);
        int i;
        for(i = 0; i < right.Count; i++)
        {
            trashes_right.Add(right[i].name);
        }
        for(i = 0; i < wrong.Count; i++)
        {
            trashes_wrong.Add(wrong[i].name);
        }
        
    }


    public void MakeTreatStage()
    {
        SceneManager.LoadScene("TreatScene");
        //TreatController.instance.InitTreatStage(currentLevel, db.GetStageItem(currentLevel));
    }

    public void LevelUp()
    {
        currentLevel += 1;
        MakeRunStage();
    }
}
