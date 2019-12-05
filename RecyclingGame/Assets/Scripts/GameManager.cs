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
    public List<Dictionary<string, object>> loadingMessages;

    // Start is called before the first frame update
    void Start()
    {
        db = new StageDB();
        db.DBInit();
        loadingMessages = CSVReader.Read("FileResources/" + "LoadingMessage");
    }

    void Update()
    {
        
        
    }

    
    public void StartGame()
    {
        currentLevel = 0;
        life = 3;
        score = 0;
        SceneManager.LoadScene("LoadingScene");
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
