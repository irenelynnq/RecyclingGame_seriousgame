using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodEndingInputController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.AudioStart(SoundManager.instance.bgmSource, SoundManager.instance.happyEnding_bgm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
