using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoingBack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= GameManager.instance.currentLevel; i++)
        {
            GameManager.instance.db.GetStageItem(i).StageItemClean();
        }
        StartTakeTime(3);
    }

    IEnumerator TakeTime(int seconds)
    {
        int count = seconds;
        while (count > 0)
        {
            yield return new WaitForSeconds(1);
            count--;
        }
        GameManager.instance.StartGame();
    }
    public void StartTakeTime(int seconds)
    {
        StartCoroutine(TakeTime(seconds));
    }
}
