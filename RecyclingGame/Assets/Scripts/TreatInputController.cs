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

    TreatState currentTreatState;
    // Start is called before the first frame update
    void Start()
    {
        treatUI = treatUIController.GetComponent<TreatUI>();
        selectBox.SetActive(false);
        currentTreatState = TreatState.stop;
        selectPosition = 1;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) && SceneManager.GetActiveScene().name == "TreatScene")
        {
            if (GameManager.instance.currentLevel == 1)
            {
                if (treatUI.keyTutorial.activeSelf == true)
                {
                    treatUI.ShowKeyTutorial(false);
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

        if(currentTreatState == TreatState.treating && treatUI.timer.currentTimerState == TimerState.counting)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                bool isDone = TreatController.instance.currentStage.treatTrashDict[selectPosition].IsTreatDone();
                if (isDone) TreatController.instance.GotAnswer(selectPosition);
                treatUI.UpdateGauge(selectPosition, TreatController.instance.GetTrashAtPosition(selectPosition));
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                switch (selectPosition)
                {
                    case 4:
                    case 8:
                    case 12:
                        selectPosition -= 3;
                        selectBox.transform.position = TreatController.instance.treatPosition[selectPosition];
                        break;
                    default:
                        selectPosition += 1;
                        selectBox.transform.position = TreatController.instance.treatPosition[selectPosition];
                        break;
                }
                treatUI.UpdateGauge(selectPosition, TreatController.instance.GetTrashAtPosition(selectPosition));
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                switch (selectPosition)
                {
                    case 1:
                    case 5:
                    case 9:
                        selectPosition += 3;
                        selectBox.transform.position = TreatController.instance.treatPosition[selectPosition];
                        break;
                    default:
                        selectPosition -= 1;
                        selectBox.transform.position = TreatController.instance.treatPosition[selectPosition];
                        break;
                }
                treatUI.UpdateGauge(selectPosition, TreatController.instance.GetTrashAtPosition(selectPosition));
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                switch (selectPosition)
                {
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                        selectPosition -= 8;
                        selectBox.transform.position = TreatController.instance.treatPosition[selectPosition];
                        break;
                    default:
                        selectPosition += 4;
                        selectBox.transform.position = TreatController.instance.treatPosition[selectPosition];
                        break;
                }
                treatUI.UpdateGauge(selectPosition, TreatController.instance.GetTrashAtPosition(selectPosition));
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                switch (selectPosition)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        selectPosition += 8;
                        selectBox.transform.position = TreatController.instance.treatPosition[selectPosition];
                        break;
                    default:
                        selectPosition -= 4;
                        selectBox.transform.position = TreatController.instance.treatPosition[selectPosition];
                        break;
                }
                treatUI.UpdateGauge(selectPosition, TreatController.instance.GetTrashAtPosition(selectPosition));
            }
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
    }

    public void StartTreating()
    {
        currentTreatState = TreatState.treating;
        treatUI.timer.SetTimerState(TimerState.counting);
    }
}
