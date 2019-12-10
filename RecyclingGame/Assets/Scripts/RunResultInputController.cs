using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunResultInputController : MonoBehaviour
{
    public GameObject menuBox;
    public GameObject menuContinue;
    public GameObject menuMain;
    public GameObject menuPointer;
    public GameObject menuPointer2;
    public MenuState menuState;

    // Start is called before the first frame update
    void Start()
    {
        menuState = MenuState.cont;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.instance.currentGameState == GameState.menu)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                MoveMenuPointer();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GameManager.instance.SetGameState(GameState.inGame);
                OutMenu();
                SoundManager.instance.FxSound(SoundManager.instance.next_fx);
                if (menuState == MenuState.cont)
                {
                    
                }
                else if (menuState == MenuState.mainTitle)
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
        else if (GameManager.instance.currentGameState == GameState.inGame)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ShowMenu();
                GameManager.instance.SetGameState(GameState.menu);
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (RunController.instance.DidPassRunStage())
                {
                    GameManager.instance.MakeTreatStage();
                }
                else
                {
                    //fail runstage
                    GameManager.instance.db.GetStageItem(GameManager.instance.currentLevel).StageItemClean();
                    GameManager.instance.MakeRunStage();
                }
            }
        }
        
    }

    public void ShowMenu()
    {
        menuBox.SetActive(true);
        menuContinue.SetActive(true);
        menuMain.SetActive(true);
        menuPointer.SetActive(true);
        menuPointer2.SetActive(false);
        menuState = MenuState.cont;
    }

    public void OutMenu()
    {
        menuBox.SetActive(false);
        menuContinue.SetActive(false);
        menuMain.SetActive(false);
        menuPointer.SetActive(false);
    }

    public void MoveMenuPointer()
    {
        Debug.Log("move!");
        SoundManager.instance.FxSound(SoundManager.instance.cursor_fx);
        if (menuState == MenuState.cont)
        {
            menuState = MenuState.mainTitle;
            menuPointer.SetActive(false);
            menuPointer2.SetActive(true);
        }
        else if (menuState == MenuState.mainTitle)
        {
            menuState = MenuState.cont;
            menuPointer.SetActive(true);
            menuPointer2.SetActive(false);
        }
    }
}
