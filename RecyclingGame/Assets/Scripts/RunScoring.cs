using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RunScoring : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*
        List<string> tempwrong = GameManager.instance.trashes_wrong;
        HashSet<string> wrong = new HashSet<string>();
        for (int i = 0; i < tempwrong.Count; i++)
        {
            wrong.Add(tempwrong[i]);
        }
        */

        int currentLevel = GameManager.instance.currentLevel;
        int rightCount = RunController.instance.trashes_right.Count;

        GetComponent<TextMeshProUGUI>().text =
            "모은 일반쓰레기 : " + rightCount + " / "
            + GameManager.instance.db.GetStageItem(currentLevel).answerCount.ToString() + "\n";

        if(rightCount < GameManager.instance.db.GetStageItem(currentLevel).pass_criteria) 
        {
            GetComponent<TextMeshProUGUI>().text += "쓰레기를 덜 주웠습니다. 다시 플레이해야 합니다.";
        }
        else
        {
            RunController.instance.PassRunStage();
        }

        /*
       foreach(var val in wrong)
        {
            GetComponent<TextMeshProUGUI>().text += val + ", ";
        }

        GetComponent<TextMeshProUGUI>().text.TrimEnd(' ', ',');
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
