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
    // Start is called before the first frame update
    void Start()
    {
        stageName.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/UI/" + "UIStageName_" + GameManager.instance.currentLevel.ToString());
        lifeCount.GetComponent<TextMeshProUGUI>().text = "X " + GameManager.instance.life.ToString();
        UpdateLife();

    }

    // Update is called once per frame
    void Update()
    {
        //이 부분에 미니맵 움직이는 거 구현
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
}
