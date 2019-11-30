using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunStageMaker : MonoBehaviour
{
    private void Awake()
    {
        RunController.instance.InitRunStage(GameManager.instance.currentLevel, GameManager.instance.db.GetStageItem(GameManager.instance.currentLevel));
        Debug.Log("init");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
