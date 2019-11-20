using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RunUI : MonoBehaviour
{
    public GameObject stageName;
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

    RectTransform mapCharTr;
    // Start is called before the first frame update
    void Start()
    {
        trashDictionary.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/Scroll/" + "TrashDictionary" + GameManager.instance.currentLevel.ToString());
        ShowDictionary(false);
        ShowKeyTutorial(false);

        ShowScroll();

        stageName.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/UI/" + "UIStageName_" + GameManager.instance.currentLevel.ToString());
        lifeCount.GetComponent<TextMeshProUGUI>().text = "X " + GameManager.instance.life.ToString();
        UpdateLife();

        mapStart = mapCharacter.GetComponent<RectTransform>().position.x;
        mapY = mapCharacter.GetComponent<RectTransform>().position.y;
        mapFinish = mapDevil.GetComponent<RectTransform>().position.x;
        start = 0;
        finish = finishPoint.GetComponent<Transform>().position.x;

        mapCharTr = mapCharacter.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        //이 부분에 미니맵 움직이는 거 구현. 계산 하자
        x = mapStart + ((playerPosition - start) * (mapFinish - mapStart) / (finish - start));
        mapCharTr.position = new Vector3(x, mapY, 0);
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
        if(GameManager.instance.currentLevel == 1)
        {
            ShowKeyTutorial(true);
        }
        ShowDictionary(true);
    }
}
