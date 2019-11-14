using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatStageMaker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TreatController.instance.InitTreatStage(GameManager.instance.currentLevel, GameManager.instance.db.GetStageItem(GameManager.instance.currentLevel));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
