using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public TextMeshProUGUI loading;
    public TextMeshProUGUI info;


    // Start is called before the first frame update
    void Start()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("FileResources/" + "Loading Message");
        loading.text = (string)data[GameManager.instance.currentLevel - 1]["message"];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
