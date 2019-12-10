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
        switch(GameManager.instance.currentLevel)
        {
            case 0:
                info.text = "플라스틱 배출 시, 일반 페트병류와 기타 플라스틱류를\r\n구분하여 배출한다.";
                break;
            case 1:
                info.text = "깨진 유리는 분리배출이 어려우므로,\r\n신문지 등으로 잘 포장한 뒤 종량제 봉투에 넣어 배출한다.";
                break;
            case 2:
                info.text = "우유팩과 종이컵은 겉면이 코팅되어 있으므로,\r\n'종이류'가 아닌 '종이팩'으로 따로 분리하여 배출한다.";
                break;
            case 3:
                info.text = "장류(ex. 쌈장), 갑각류/어패류의 껍데기, 차 찌꺼기,\r\n한약재 등은 '음식물쓰레기'로의 분리배출이 불가하다.";
                break;
            case 4:
                info.text = "페트병 배출 시 내부를 잘 씻어내고, 뚜껑이나 부착라벨 등\r\n본체와 다른 재질을 제거한 뒤 배출한다.";
                break;
            default:
                break;
        }
        //info.text = (string)(GameManager.instance.loadingMessages[GameManager.instance.currentLevel]["message"]);
        StartTakeTime(5);
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
