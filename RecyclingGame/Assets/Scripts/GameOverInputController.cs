using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverInputController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            for (int i = 1; i <= GameManager.instance.currentLevel; i++)
            {
                GameManager.instance.db.GetStageItem(i).StageItemClean();
            }
            
            GameManager.instance.StartGame();
        }
        
    }


}
