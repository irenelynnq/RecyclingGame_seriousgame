using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TreatScoring : MonoBehaviour
{
    public GameObject title;

    public List<GameObject> treatedImage;
    public List<GameObject> hearts;
    public GameObject full;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.FxSound(SoundManager.instance.clear_fx);
        int i;
        for(i=0; i<hearts.Count; i++)
        {
            hearts[i].SetActive(false);
        }
        int currentLevel = GameManager.instance.currentLevel;
        GameManager.instance.life = Mathf.Min(GameManager.instance.life + CalculateLifeAdded(TreatController.instance.playerAnswerCount), 5);
        if (GameManager.instance.life == 5) full.SetActive(true);

        title.GetComponent<TextMeshProUGUI>().text = "STAGE " + currentLevel + " - 2 CLEAR";

        List<string> answerSpriteNames = TreatController.instance.playerAnswerSpriteNames;
        for (i=0; i< TreatController.instance.playerAnswerCount; i++)
        {
            Sprite trashSprite = Resources.Load<Sprite>("Art/Trash/" + answerSpriteNames[i]);
            if (trashSprite == null) trashSprite = Resources.Load<Sprite>("Art/Trash/" + "Zright");
            treatedImage[i].GetComponent<Image>().sprite = trashSprite;
        }

        for(; i<treatedImage.Count; i++)
        {
            treatedImage[i].SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int CalculateLifeAdded(int answerCount)
    {
        int lifeAdded;
        switch (answerCount)
        {
            case 3:
            case 4:
                lifeAdded = 1;
                if (GameManager.instance.life < 5)
                {
                    hearts[0].SetActive(true);
                }
                break;
            case 5:
            case 6:
                lifeAdded = 2;
                if (GameManager.instance.life < 5) hearts[0].SetActive(true);
                if (GameManager.instance.life < 4) hearts[1].SetActive(true);
                break;
            default:
                lifeAdded = 0;
                break;
        }
        return lifeAdded;
    }
}
