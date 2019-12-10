using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GoodEndingInputController : MonoBehaviour
{
    public GameObject background;
    public GameObject text;
    TextMeshProUGUI textBox;
    public GameObject enter;

    private List<string> ending1 = new List<string>
    { "글쎄. 모든 관문이 끝나는 순간 갑자기 온 세상이 하얘졌고, 그 다음은 잘 기억이 안 난다. 그냥 눈을 떠보니 다시 내 방에 얌전히 누워있었을 뿐. "
    };
    private List<string> ending2 = new List<string>
    {"내가 정말로 저 세상에 다녀온 건지, 아님 아주 긴 꿈을 꾸었던 건지는 아직도 잘 모르겠다.",
        "그치만 분명한 건, 내가 아주 달라졌다는 거다. 난 이제 더 이상 쓰레기라면 손에 잡히는 대로 막 버리던 그때의 내가 아니다.",
        "분리수거가 아무리 귀찮아 봤자, 그 자체로 이미 지옥 같았던 그때의 고생에 비하면 아무것도 아니니까."
    };
    private List<string> ending3 = new List<string>
    { "그리고 또 하나 깨달은 사실. 사람들은 쓰레기를 정말 함부로 버린다.",
        "방금 그 사람은 바나나 껍질을 일반쓰레기로 버렸고, 저 사람은 씻지도 않은 페트병을 그대로 버렸다.",
        "물론 여기서 나 말곤 아무도 모른다. 결국 우리는 모두 죗값을 치르게 될 거라는 걸.",
        " 아아, 잠깐, 아니지..."
    };
    private List<string> ending4 = new List<string>
    { "이제 당신도 알잖아…?" };

    private List<List<string>> endings = new List<List<string>>();
    private int textIndex = 0;
    private int sceneIndex = 0;
    private bool canEnter = true;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.instance.AudioStart(SoundManager.instance.bgmSource, SoundManager.instance.happyEnding_bgm);
        endings.Add(ending1);
        endings.Add(ending2);
        endings.Add(ending3);
        endings.Add(ending4);
        textBox = text.GetComponent<TextMeshProUGUI>();
        StartDialogue(textBox, endings[sceneIndex][textIndex], enter);
    }

    // Update is called once per frame
    void Update()
    {
        if (canEnter && Input.GetKeyDown(KeyCode.Return))
        {
            enter.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 150);
        }
        if (canEnter && Input.GetKeyUp(KeyCode.Return))
        {
            enter.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);

            textIndex++;

            if (textIndex == endings[sceneIndex].Count)
            {
                sceneIndex++;
                if (sceneIndex > 3)
                {
                    //end of ending scene
                    SoundManager.instance.AudioStop(SoundManager.instance.bgmSource);
                    for (int i = 1; i <= GameManager.instance.currentLevel; i++)
                    {
                        GameManager.instance.db.GetStageItem(i).StageItemClean();
                    }
                    GameManager.instance.is_first_run = true;
                    GameManager.instance.is_first_treat = true;
                    SceneManager.LoadScene("TitleScene");
                }
                else
                {
                    if(sceneIndex == 3)
                    {
                        SoundManager.instance.AudioStop(SoundManager.instance.bgmSource);
                    }
                    background.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/CutScene/Ending/" + "EndingIllust" + (sceneIndex + 1).ToString());
                    textIndex = 0;
                    StartDialogue(textBox, endings[sceneIndex][textIndex], enter);
                }
            }
            else if(textIndex < endings[sceneIndex].Count)
            {
                StartDialogue(textBox, endings[sceneIndex][textIndex], enter);
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
        canEnter = false;
        enter.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 150);
        for (int i = 0; i < str.Length; i++)
        {
            textBox.text += str[i];
            yield return new WaitForSeconds(0.01f);
        }
        enter.GetComponent<TextMeshProUGUI>().color = new Color32(0, 0, 0, 255);
        canEnter = true;
    }
}
