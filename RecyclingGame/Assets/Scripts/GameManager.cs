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

public enum MenuState
{
    cont,
    mainTitle
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public GameState currentGameState;
    public GameScene currentGameScene = GameScene.run;
    public int currentLevel;
    public int life;
    public int score;
    public bool is_first_run;
    public bool is_first_treat;

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
        is_first_run = true;
        is_first_treat = true;
        currentGameState = GameState.inGame;
    }
    
    public StageDB db;
    //public List<Dictionary<string, object>> loadingMessages;

    // Start is called before the first frame update
    void Start()
    {
        db = new StageDB();
        db.DBInit();
        //loadingMessages = CSVReader.Read("FileResources/" + "LoadingMessage");
    }

    void Update()
    {
        
        
    }

    
    public void StartGame()
    {
        currentLevel = 0;
        life = 3;
        score = 0;
        if (is_first_run)
        {
            SceneManager.LoadScene("LoadingScene");
        }
        else
        {
            LevelUp();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
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

    


    public void MakeTreatStage()
    {
        SceneManager.LoadScene("TreatScene");
    }

    public void LevelUp()
    {
        currentLevel += 1;
        if (currentLevel < 6) MakeRunStage();
        else GoodEnding();
    }

    public void GoodEnding()
    {
        SceneManager.LoadScene("GoodEndingScene");
    }
}
