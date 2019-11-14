using System.Collections;
using System.Collections.Generic;
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
    public Vector3 selectOffset;
    public GameObject selectBox;

    public GameObject timerDisplay;
    public Timer timer;

    public GameObject description;

    TreatState currentTreatState;
    // Start is called before the first frame update
    void Start()
    {
        currentTreatState = TreatState.stop;
        selectPosition = 1;
        selectOffset = new Vector3(0, 1.22f);
        timer = timerDisplay.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S) && SceneManager.GetActiveScene().name == "TreatScene")
        {
            description.SetActive(false);
            currentTreatState = TreatState.treating;
            selectBox.SetActive(true);
            timer.SetTimerState(TimerState.counting);
        }

        if(currentTreatState == TreatState.treating && timer.currentTimerState == TimerState.counting)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                bool isDone = TreatController.instance.currentStage.treatTrashDict[selectPosition].IsTreatDone();
                if (isDone) TreatController.instance.GotAnswer(selectPosition);
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                switch (selectPosition)
                {
                    case 5:
                    case 10:
                        break;
                    default:
                        selectPosition += 1;
                        selectBox.transform.position = TreatController.instance.treatPosition[selectPosition] + selectOffset;
                        break;
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                switch (selectPosition)
                {
                    case 1:
                    case 6:
                        break;
                    default:
                        selectPosition -= 1;
                        selectBox.transform.position = TreatController.instance.treatPosition[selectPosition] + selectOffset;
                        break;
                }
            }
            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                switch (selectPosition)
                {
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        break;
                    default:
                        selectPosition += 5;
                        selectBox.transform.position = TreatController.instance.treatPosition[selectPosition] + selectOffset;
                        break;
                }
            }
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                switch (selectPosition)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        break;
                    default:
                        selectPosition -= 5;
                        selectBox.transform.position = TreatController.instance.treatPosition[selectPosition] + selectOffset;
                        break;
                }
            }
        }
        

        
    }


}
