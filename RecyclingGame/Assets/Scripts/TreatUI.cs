using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreatUI : MonoBehaviour
{
    public GameObject stageName;
    public List<GameObject> lifeHearts;

    public GameObject timerDisplay;
    public Timer timer;

    public GameObject countDown;

    public GameObject keyTutorial;

    public GameObject gaugeRect;
    public GameObject gaugeFill;
    Dictionary<int, Vector3> gaugeDict;
    
    // Start is called before the first frame update
    void Start()
    {
        InitGaugeDict();
        timer = timerDisplay.GetComponent<Timer>();
        countDown.SetActive(false);
        ShowScroll();

        stageName.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/UI/" + "UITreatName_" + GameManager.instance.currentLevel.ToString());
        UpdateLife();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLife()
    {
        int i;
        for (i = 0; i < 5; i++)
        {
            lifeHearts[i].SetActive(false);
        }
        for (i = 0; i < GameManager.instance.life; i++)
        {
            lifeHearts[i].SetActive(true);
        }
    }

    public void ShowKeyTutorial(bool state)
    {
        keyTutorial.SetActive(state);
    }

    public void ShowScroll()
    {
        if (GameManager.instance.currentLevel == 1)
        {
            ShowKeyTutorial(true);
        }
    }

    public void UpdateGauge(int i, Trash trash)
    {
        gaugeRect.GetComponent<RectTransform>().position = gaugeDict[i];
        gaugeFill.GetComponent<RectTransform>().position = gaugeDict[i];
        gaugeFill.GetComponent<Image>().fillAmount = (float)trash.GetHitCount() / 15;
    }

    void InitGaugeDict()
    {
        gaugeDict = new Dictionary<int, Vector3>();
        gaugeDict.Add(1, calVec(new Vector3(-6.32f, 1.28f)));
        gaugeDict.Add(2, calVec(new Vector3(-2.11f, 1.28f)));
        gaugeDict.Add(3, calVec(new Vector3(2.11f, 1.28f)));
        gaugeDict.Add(4, calVec(new Vector3(6.32f, 1.28f)));

        gaugeDict.Add(5, calVec(new Vector3(-6.32f, -0.88f)));
        gaugeDict.Add(6, calVec(new Vector3(-2.11f, -0.88f)));
        gaugeDict.Add(7, calVec(new Vector3(2.11f, -0.88f)));
        gaugeDict.Add(8, calVec(new Vector3(6.32f, -0.88f)));

        gaugeDict.Add(9, calVec(new Vector3(-6.32f, -3.04f)));
        gaugeDict.Add(10, calVec(new Vector3(-2.11f, -3.04f)));
        gaugeDict.Add(11, calVec(new Vector3(2.11f, -3.04f)));
        gaugeDict.Add(12, calVec(new Vector3(6.32f, -3.04f)));
    }
    
    Vector3 calVec(Vector3 posVec)
    {
        return new Vector3(calX(posVec.x), calY(posVec.y));
    }
    float calX(float posX)
    {
        return (posX + 1.38f) * 10 * 4;
    }
    float calY(float posY)
    {
        return (posY * 10 * 4) + 3;
    }
}
