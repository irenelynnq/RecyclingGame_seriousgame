using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreatUI : MonoBehaviour
{
    public GameObject stageName;
    public GameObject stageNumber;
    public List<GameObject> lifeHearts;

    public GameObject timerDisplay;
    public Timer timer;

    public GameObject countDown;

    public GameObject keyTutorial;
    public GameObject trashDictionary;

    public GameObject gaugeRect;
    public GameObject gaugeFill;
    Dictionary<int, Vector3> gaugeDict;

    public GameObject gaugeIndicator;

    public GameObject menuBox;
    public GameObject menuContinue;
    public GameObject menuMain;
    public GameObject menuPointer;
    public GameObject menuPointer2;
    public MenuState menuState;

    // Start is called before the first frame update
    void Start()
    {
        ShowDictionary(false);
        ShowKeyTutorial(false);
        InitGaugeDict();
        timer = timerDisplay.GetComponent<Timer>();
        countDown.SetActive(false);
        trashDictionary.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/Scroll/" + "TrashDictionary" + GameManager.instance.currentLevel.ToString() + "-2");
        ShowScroll();

        stageName.GetComponent<TextMeshProUGUI>().text = GameManager.instance.db.GetStageItem(GameManager.instance.currentLevel).preprocess;
        stageNumber.GetComponent<TextMeshProUGUI>().text = GameManager.instance.currentLevel.ToString();
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

    public void ShowDictionary(bool state)
    {
        trashDictionary.SetActive(state);
    }

    public void ShowKeyTutorial(bool state)
    {
        keyTutorial.SetActive(state);
    }

    public void ShowScroll()
    {
        if (GameManager.instance.is_first_treat == true)
        {
            ShowKeyTutorial(true);
        }
        else
        {
            ShowDictionary(true);
        }
        
    }

    public void UpdateGauge(int i, Trash trash)
    {
        gaugeIndicator.GetComponent<Transform>().position = gaugeDict[i];
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
        return (posX + 1.38f);
        //return posX;
    }
    float calY(float posY)
    {
        return posY;
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
