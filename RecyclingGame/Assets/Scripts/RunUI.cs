using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RunUI : MonoBehaviour
{
    public GameObject stageName;
    public GameObject stageNumber;
    public GameObject lifeCount;
    public List<GameObject> lifeHearts;

    public GameObject mapCharacter;
    public GameObject mapDevil;
    public GameObject finishPoint;

    public GameObject countDown;

    public GameObject trashDictionary;
    public GameObject keyTutorial;

    public float playerPosition;
    private float mapStart;
    private float mapY;
    private float mapFinish;
    private float start;
    private float finish;
    private float x;

    public GameObject menuBox;
    public GameObject menuContinue;
    public GameObject menuMain;
    public GameObject menuPointer;
    public GameObject menuPointer2;
    public MenuState menuState;

    RectTransform mapCharTr;
    // Start is called before the first frame update
    void Start()
    {
        ShowDictionary(false);
        ShowKeyTutorial(false);
        countDown.SetActive(false);
        trashDictionary.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/Scroll/" + "TrashDictionary" + GameManager.instance.currentLevel.ToString() + "-1");
        ShowScroll();

        stageName.GetComponent<TextMeshProUGUI>().text = GameManager.instance.db.GetStageItem(GameManager.instance.currentLevel).name;
        stageNumber.GetComponent<TextMeshProUGUI>().text = GameManager.instance.currentLevel.ToString();
        lifeCount.GetComponent<TextMeshProUGUI>().text = "X " + GameManager.instance.life.ToString();
        UpdateLife();

        mapStart = mapCharacter.GetComponent<RectTransform>().position.x;
        print("minimap" + mapStart.ToString());
        mapY = mapCharacter.GetComponent<RectTransform>().position.y;
        mapFinish = mapDevil.GetComponent<RectTransform>().position.x;
        start = 0;
        finish = finishPoint.GetComponent<Transform>().position.x;

        mapCharTr = mapCharacter.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //이 부분에 미니맵 움직이는 거 구현. 계산 하자
        x = mapStart + ((playerPosition - start) * (mapFinish - mapStart) / (finish - start));
        if (mapCharTr != null)
        {
            mapCharTr.position = new Vector3(x, mapY, 0);
        }
    }

    public void UpdateLife()
    {
        lifeCount.GetComponent<TextMeshProUGUI>().text = "X " + GameManager.instance.life.ToString();
        int i;
        for(i=0; i<5; i++)
        {
            lifeHearts[i].SetActive(false);
        }
        for(i=0; i<GameManager.instance.life; i++)
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
        if (GameManager.instance.is_first_run == true)
        {
            ShowKeyTutorial(true);
        }
        else
        {
            ShowDictionary(true);
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
