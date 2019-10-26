using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RunScoring : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        GetComponent<TextMeshProUGUI>().text =
            "Right Trash : " + GameManager.instance.playerController.trashCollector.collected_right.Count + "\n" +
            "Wrong Trash : " + GameManager.instance.playerController.trashCollector.collected_wrong.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
