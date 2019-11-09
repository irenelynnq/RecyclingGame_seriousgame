using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunUI : MonoBehaviour
{
    public GameObject stageTitle;
    public GameObject lifeCount;
    // Start is called before the first frame update
    void Start()
    {
        stageTitle.GetComponent<TextMeshProUGUI>().text = GameManager.instance.db.stageList[GameManager.instance.currentLevel-1].name;
        lifeCount.GetComponent<TextMeshProUGUI>().text = "X " + GameManager.instance.life.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //이 부분에 미니맵 움직이는 거 구현
    }

    public void UpdateLife()
    {
        lifeCount.GetComponent<TextMeshProUGUI>().text = "X " + GameManager.instance.life.ToString();
    }
}
