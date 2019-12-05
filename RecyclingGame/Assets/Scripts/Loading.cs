using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public TextMeshProUGUI loading;
    public TextMeshProUGUI info;


    // Start is called before the first frame update
    void Start()
    {
        info.text = (string)(GameManager.instance.loadingMessages[GameManager.instance.currentLevel]["message"]);
        StartTakeTime(Random.Range(3, 6));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator TakeTime(int seconds)
    {
        int count = seconds;
        while (count > 0)
        {
            yield return new WaitForSeconds(1);
            count--;
        }
        GameManager.instance.LevelUp();
    }
    public void StartTakeTime(int seconds)
    {
        StartCoroutine(TakeTime(seconds));
    }
}
