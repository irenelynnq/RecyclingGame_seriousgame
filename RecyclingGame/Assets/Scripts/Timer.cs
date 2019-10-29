using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum TimerState
{
    stop,
    counting
}

public class Timer : MonoBehaviour
{
    public float LimitTime;
    public TextMeshProUGUI text_Timer;

    public TimerState currentTimerState;
    public void SetTimerState(TimerState newstate)
    {
        currentTimerState = newstate;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTimerState = TimerState.stop;

    }



    // Update is called once per frame
    void Update()
    {
        if(currentTimerState == TimerState.counting)
        {
            LimitTime -= Time.deltaTime;
            text_Timer.text = "제한시간 : " + Mathf.Round(LimitTime);
        }

        if(LimitTime <= 0)
        {
            SetTimerState(TimerState.stop);
            SceneManager.LoadScene("TreatResultScene");

        }
    }
}
