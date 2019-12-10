using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverInputController : MonoBehaviour
{
    public GameObject text;
    TextMeshProUGUI textBox;
    public GameObject enter;
    private List<string> gameOverText = new List<string> { "아, 그때 좀 더 잘할걸.", "그런데 나 지금 생각났어.", "지금 있는 여기...", "죗값을 다 치르기 전까진 못 나가." };
    private int textIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.AudioStart(SoundManager.instance.bgmSource, SoundManager.instance.badEnding_bgm);
        textBox = text.GetComponent<TextMeshProUGUI>();
        StartDialogue(textBox, gameOverText[textIndex], enter);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            enter.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 150);
        }
        if (Input.GetKeyUp(KeyCode.Return))
        {
            enter.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);
            textIndex++;
            /*
            if (textIndex >= gameOverText.Count)
            {
                SoundManager.instance.AudioStop(SoundManager.instance.bgmSource);
                SceneManager.LoadScene("GoingBackScene");
            }
            */
            if (textIndex == gameOverText.Count-1)
            {
                enter.SetActive(false);
                StartDialogue(textBox, gameOverText[textIndex], enter);
                StartTakeTime(3);
            }
            else if (textIndex < gameOverText.Count -1)
            {
                StartDialogue(textBox, gameOverText[textIndex], enter);
            }
                        
        }
        
    }

    public void StartDialogue(TextMeshProUGUI textBox, string str, GameObject enter)
    {
        textBox.text = "";
        StartCoroutine(DialogueCoroutine(textBox, str, enter));
    }

    IEnumerator DialogueCoroutine(TextMeshProUGUI textBox, string str, GameObject enter)
    {
        enter.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 150);
        for (int i=0; i<str.Length; i++)
        {
            textBox.text += str[i];
            yield return new WaitForSeconds(0.01f);
        }
        enter.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);
    }

    IEnumerator TakeTime(int seconds)
    {
        int count = seconds;
        while (count > 0)
        {
            
            yield return new WaitForSeconds(1);
            count--;
            
        }
        SoundManager.instance.AudioStop(SoundManager.instance.bgmSource);
        SceneManager.LoadScene("GoingBackScene");
       

    }
    public void StartTakeTime(int seconds)
    {
        StartCoroutine(TakeTime(seconds));
    }


}
