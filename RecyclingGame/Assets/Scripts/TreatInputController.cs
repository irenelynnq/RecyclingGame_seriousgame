using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TreatState
{
    stop,
    treating
}

public class TreatInputController : MonoBehaviour
{
    public int selectPosition;
    public GameObject selectBox;

    public GameObject treatUIController;
    public TreatUI treatUI;
    public List<GameObject> successMarks;

    TreatState currentTreatState;

    private bool canMenu;

    // Start is called before the first frame update
    void Start()
    {
        treatUI = treatUIController.GetComponent<TreatUI>();
        selectBox.SetActive(false);
        currentTreatState = TreatState.stop;
        selectPosition = 1;
        canMenu = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "TreatScene")
        {
            if (GameManager.instance.currentGameState == GameState.menu)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    treatUI.MoveMenuPointer();
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    GameManager.instance.SetGameState(GameState.inGame);
                    treatUI.OutMenu();
                    SoundManager.instance.FxSound(SoundManager.instance.next_fx);
                    if (treatUI.menuState == MenuState.cont)
                    {
                        if (!(treatUI.keyTutorial.activeSelf) && !(treatUI.trashDictionary.activeSelf))
                        {
                            treatUI.countDown.SetActive(true);
                            StartCountDownDisplay(4);
                        }
                    }
                    else if (treatUI.menuState == MenuState.mainTitle)
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
                if (Input.GetKeyDown(KeyCode.Escape) && canMenu)
                {
                    treatUI.timer.SetTimerState(TimerState.stop);
                    currentTreatState = TreatState.stop;
                    treatUI.ShowMenu();
                    GameManager.instance.SetGameState(GameState.menu);
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (GameManager.instance.is_first_treat == true)
                    {
                        if (treatUI.keyTutorial.activeSelf == true)
                        {
                            treatUI.ShowDictionary(true);
                            treatUI.ShowKeyTutorial(false);
                            GameManager.instance.is_first_treat = false;
                        }
                        else if (treatUI.trashDictionary.activeSelf == true)
                        {
                            treatUI.ShowDictionary(false);
                            selectBox.SetActive(true);
                            treatUI.UpdateGauge(selectPosition, TreatController.instance.GetTrashAtPosition(selectPosition));
                            treatUI.gaugeFill.SetActive(true);
                            treatUI.gaugeRect.SetActive(true);
                            treatUI.countDown.SetActive(true);
                            SoundManager.instance.AudioVolume(SoundManager.instance.bgmSource, 1f);
                            StartCountDownDisplay(4);
                        }
                    }
                    else if (treatUI.trashDictionary.activeSelf == true)
                    {
                        treatUI.ShowDictionary(false);
                        selectBox.SetActive(true);
                        treatUI.UpdateGauge(selectPosition, TreatController.instance.GetTrashAtPosition(selectPosition));
                        treatUI.gaugeFill.SetActive(true);
                        treatUI.gaugeRect.SetActive(true);
                        treatUI.countDown.SetActive(true);
                        SoundManager.instance.AudioVolume(SoundManager.instance.bgmSource, 1f);
                        StartCountDownDisplay(4);
                    }

                }

                if (currentTreatState == TreatState.treating && treatUI.timer.currentTimerState == TimerState.counting)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        bool isDone = TreatController.instance.currentStage.treatTrashDict[selectPosition].IsTreatDone();
                        if (isDone)
                        {
                            successMarks[selectPosition - 1].SetActive(true);
                            TreatController.instance.GotAnswer(selectPosition);
                        }
                        treatUI.UpdateGauge(selectPosition, TreatController.instance.GetTrashAtPosition(selectPosition));
                    }
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        switch (selectPosition)
                        {
                            case 4:
                            case 8:
                            case 12:
                                selectPosition -= 3;
                                selectBox.transform.position = TreatController.instance.treatPosition[selectPosition] + new Vector3(0f, 1.1f);
                                break;
                            default:
                                selectPosition += 1;
                                selectBox.transform.position = TreatController.instance.treatPosition[selectPosition] + new Vector3(0f, 1.1f);
                                break;
                        }
                        treatUI.UpdateGauge(selectPosition, TreatController.instance.GetTrashAtPosition(selectPosition));
                    }
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        switch (selectPosition)
                        {
                            case 1:
                            case 5:
                            case 9:
                                selectPosition += 3;
                                selectBox.transform.position = TreatController.instance.treatPosition[selectPosition] + new Vector3(0f, 1.1f);
                                break;
                            default:
                                selectPosition -= 1;
                                selectBox.transform.position = TreatController.instance.treatPosition[selectPosition] + new Vector3(0f, 1.1f);
                                break;
                        }
                        treatUI.UpdateGauge(selectPosition, TreatController.instance.GetTrashAtPosition(selectPosition));
                    }
                    if (Input.GetKeyDown(KeyCode.DownArrow))
                    {
                        switch (selectPosition)
                        {
                            case 9:
                            case 10:
                            case 11:
                            case 12:
                                selectPosition -= 8;
                                selectBox.transform.position = TreatController.instance.treatPosition[selectPosition] + new Vector3(0f, 1.1f);
                                break;
                            default:
                                selectPosition += 4;
                                selectBox.transform.position = TreatController.instance.treatPosition[selectPosition] + new Vector3(0f, 1.1f);
                                break;
                        }
                        treatUI.UpdateGauge(selectPosition, TreatController.instance.GetTrashAtPosition(selectPosition));
                    }
                    if (Input.GetKeyDown(KeyCode.UpArrow))
                    {
                        switch (selectPosition)
                        {
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                                selectPosition += 8;
                                selectBox.transform.position = TreatController.instance.treatPosition[selectPosition] + new Vector3(0f, 1.1f);
                                break;
                            default:
                                selectPosition -= 4;
                                selectBox.transform.position = TreatController.instance.treatPosition[selectPosition] + new Vector3(0f, 1.1f);
                                break;
                        }
                        treatUI.UpdateGauge(selectPosition, TreatController.instance.GetTrashAtPosition(selectPosition));
                    }
                }
            }
            
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
                treatUI.countDown.GetComponent<TextMeshProUGUI>().text = "START!";
                SoundManager.instance.FxSound(SoundManager.instance.start_fx);
            }
            else treatUI.countDown.GetComponent<TextMeshProUGUI>().text = (count - 1).ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        treatUI.countDown.SetActive(false);
        StartTreating();
        canMenu = true;
    }

    public void StartTreating()
    {
        currentTreatState = TreatState.treating;
        treatUI.timer.SetTimerState(TimerState.counting);
    }
}
