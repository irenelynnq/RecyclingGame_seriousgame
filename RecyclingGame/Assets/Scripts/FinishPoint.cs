using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        if(collision.gameObject.tag == "Player")
        {
            RunController.instance.GetRunResult(obj.GetComponentInChildren<TrashCollector>().collected_right, obj.GetComponentInChildren<TrashCollector>().collected_wrong);
            SoundManager.instance.AudioStop(SoundManager.instance.bgmSource);
            SceneManager.LoadScene("RunResultScene");
        }
    }
}
