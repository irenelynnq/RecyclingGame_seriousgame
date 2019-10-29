using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreatScoring : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "포장해야 할 쓰레기 " + GameManager.instance.db.GetStageItem(GameManager.instance.currentLevel).treatAnswerCount.ToString()
            + "개 중 " + TreatController.instance.playerAnswerCount.ToString() + "개를 포장했다!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
