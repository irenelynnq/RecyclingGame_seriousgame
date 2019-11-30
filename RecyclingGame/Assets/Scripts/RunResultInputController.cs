using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunResultInputController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (RunController.instance.DidPassRunStage())
            {
                GameManager.instance.MakeTreatStage();
            }
            else
            {
                //fail runstage
                GameManager.instance.db.GetStageItem(GameManager.instance.currentLevel).StageItemClean();
                GameManager.instance.MakeRunStage();
            }
        }
    }
}
