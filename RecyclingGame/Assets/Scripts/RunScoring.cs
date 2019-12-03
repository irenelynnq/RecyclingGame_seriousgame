using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RunScoring : MonoBehaviour
{

    public GameObject title;
    public GameObject enter;

    public GameObject notEnough;
    public GameObject background;

    public List<GameObject> collectedImage;
    public List<GameObject> uncollectedImage;

    // Start is called before the first frame update
    void Start()
    {
        
        int currentLevel = GameManager.instance.currentLevel;
        int rightCount = RunController.instance.trashes_right.Count;
        int i;

        if (rightCount < GameManager.instance.db.GetStageItem(currentLevel).pass_criteria) 
        {
            SoundManager.instance.FxSound(SoundManager.instance.fail_fx);
            background.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/Background/" + "map1re");
            for (i = 0; i < collectedImage.Count; i++)
            {
                collectedImage[i].GetComponent<RectTransform>().position -= new Vector3(0, 15f);
            }
            title.GetComponent<TextMeshProUGUI>().color = Color.red;
            title.GetComponent<TextMeshProUGUI>().text = "STAGE " + currentLevel.ToString() + " - 1 FAIL";
            notEnough.SetActive(true);
            enter.GetComponent<TextMeshProUGUI>().text = "엔터를 눌러 돌아가 쓰레기를 다시 주워오거라!";

        }
        else
        {
            SoundManager.instance.FxSound(SoundManager.instance.clear_fx);
            RunController.instance.PassRunStage();
            notEnough.SetActive(false);
            title.GetComponent<TextMeshProUGUI>().text = "STAGE " + currentLevel.ToString() + " - 1 CLEAR";
            enter.GetComponent<TextMeshProUGUI>().text = "엔터를 눌러 넘어가도록 하라!";
            
        }

        HashSet<string> collectedNames = new HashSet<string>();
        HashSet<string> uncollectedNames = new HashSet<string>();
        Dictionary<int, string> allRights = new Dictionary<int, string>(GameManager.instance.db.GetStageItem(currentLevel).answerSpriteNameDict);

        List<int> collectedTrash = RunController.instance.trashes_right;

        
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
            if (trashSprite == null) trashSprite = Resources.Load<Sprite>("Art/Trash/" + "Zright");
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
            if (trashSprite == null) trashSprite = Resources.Load<Sprite>("Art/Trash/" + "Zright");
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
