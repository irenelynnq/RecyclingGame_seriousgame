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
        SoundManager.instance.AudioVolume(SoundManager.instance.bgmSource, 0.3f);
        SoundManager.instance.AudioStart(SoundManager.instance.bgmSource, SoundManager.instance.run_bgm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
