using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatStageMaker : MonoBehaviour
{
    private void Awake()
    {
        TreatController.instance.InitTreatStage(GameManager.instance.currentLevel, GameManager.instance.db.GetStageItem(GameManager.instance.currentLevel));
    }
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.AudioVolume(SoundManager.instance.bgmSource, 0.3f);
        SoundManager.instance.AudioStart(SoundManager.instance.bgmSource, SoundManager.instance.treat_bgm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
