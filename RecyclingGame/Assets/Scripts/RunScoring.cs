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

        GetComponent<TextMeshProUGUI>().text =
            "모은 일반쓰레기 : " + GameManager.instance.trashes_right.Count + " / "
            + GameManager.instance.db.GetStageItem(GameManager.instance.currentLevel).answerCount.ToString() + "\n";


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
