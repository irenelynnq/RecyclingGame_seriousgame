using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RunScoring : MonoBehaviour
{

    public GameObject title;
    public GameObject enter;

    public List<GameObject> collectedImage;
    public List<GameObject> uncollectedImage;

    // Start is called before the first frame update
    void Start()
    {
        
        int currentLevel = GameManager.instance.currentLevel;
        int rightCount = RunController.instance.trashes_right.Count;

        title.GetComponent<TextMeshProUGUI>().text = "STAGE " + currentLevel + " - 1 CLEAR";

        if(rightCount < GameManager.instance.db.GetStageItem(currentLevel).pass_criteria) 
        {
            enter.GetComponent<TextMeshProUGUI>().text = "";
        }
        else
        {
            enter.GetComponent<TextMeshProUGUI>().text = "엔터를 눌러 넘어가도록 하라!";
            RunController.instance.PassRunStage();
        }

        HashSet<string> collectedNames = new HashSet<string>();
        HashSet<string> uncollectedNames = new HashSet<string>();
        Dictionary<int, string> allRights = new Dictionary<int, string>(GameManager.instance.db.GetStageItem(currentLevel).answerSpriteNameDict);

        List<int> collectedTrash = RunController.instance.trashes_right;

        int i;
        for (i = 0; i < collectedTrash.Count; i++)
        {
            if(allRights.ContainsKey(collectedTrash[i]))
            {
                collectedNames.Add(allRights[collectedTrash[i]]);
                allRights.Remove(collectedTrash[i]);
            }
        }

        foreach (string name in allRights.Values)
        {
            uncollectedNames.Add(name);
        }

        i = 0;
        foreach (string name in collectedNames)
        {
            Sprite trashSprite = Resources.Load<Sprite>(name);
            if (trashSprite == null) trashSprite = Resources.Load<Sprite>("Art/Trash/" + "Zwrong");
            collectedImage[i].GetComponent<Image>().sprite = trashSprite;
            i++;
        }
        for (; i < 6; i++)
        {
            collectedImage[i].SetActive(false);
        }

        i = 0;
        foreach (string name in uncollectedNames)
        {
            Sprite trashSprite = Resources.Load<Sprite>(name);
            if (trashSprite == null) trashSprite = Resources.Load<Sprite>("Art/Trash/" + "Zwrong");
            uncollectedImage[i].GetComponent<Image>().sprite = trashSprite;
            i++;
        }
        for (; i < 6; i++)
        {
            uncollectedImage[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
