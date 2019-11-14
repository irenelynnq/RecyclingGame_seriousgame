using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreatScoring : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.life = Mathf.Min(GameManager.instance.life + CalculateLifeAdded(TreatController.instance.playerAnswerCount), 5);
        GetComponent<TextMeshProUGUI>().text = "포장해야 할 쓰레기 " + GameManager.instance.db.GetStageItem(GameManager.instance.currentLevel).treatAnswerCount.ToString()
            + "개 중 " + TreatController.instance.playerAnswerCount.ToString() + "개를 포장했다!\n목숨이 " + GameManager.instance.life.ToString() + "개가 되었다!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int CalculateLifeAdded(int answerCount)
    {
        int lifeAdded;
        switch (answerCount)
        {
            case 3:
            case 4:
                lifeAdded = 1;
                break;
            case 5:
            case 6:
                lifeAdded = 2;
                break;
            default:
                lifeAdded = 0;
                break;
        }
        return lifeAdded;
    }
}
