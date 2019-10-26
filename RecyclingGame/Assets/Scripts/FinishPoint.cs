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
            GameManager.instance.GetRunResult(obj.GetComponentInChildren<TrashCollector>().collected_right, obj.GetComponentInChildren<TrashCollector>().collected_wrong);
            SceneManager.LoadScene("RunResultScene");
            //gamemanger가 player한테 긁어와야 하나???
        }
    }
}
