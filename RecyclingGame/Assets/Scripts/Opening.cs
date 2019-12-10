using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    public GameObject background;
    public GameObject text;
    TextMeshProUGUI textBox;
    public GameObject enter;

    private List<string> opening1 = new List<string>
    { "이곳에 온지 오늘로 꼭 44일째다.",
        "그러니까 44일 전 나는 방안에 가득한 쓰레기를 치워야 했고, 평소처럼 봉투에 대충 욱여 넣은 다음, 밖에다 몰래 던져두고 돌아왔다. 분리수거? 춥고 귀찮은데 알게 뭐야.",
        "다만 평소와 조금 다른 게 있었다면, 그날은 돌아오는 길에 웬 파란 차가 나를 덮쳤다는 것."
    };
    private List<string> opening2 = new List<string>
    { "사람이 죽으면 49일 동안 자신의 죗값을 치러야 한다. 그리고 내 죄목은 '쓰레기를 함부로 버렸다'는 거다.",
        "그게 그렇게 큰 죄냐고? 나도 그땐 몰랐다.",
        "나 때문에 바다거북이 플라스틱으로 배를 채우고, 미화원 아저씨가 깨진 유리에 손을 다치고, 소각장에선 재활용할 수 있는 쓰레기까지도 모두 태워버려서 공기가 아주 나빠졌다는 걸.",
        "무튼 지난 44일 간 나는 발이 부서져라 이곳을 돌아다니면서, 살아생전 아무렇게나 버린 쓰레기들을 모두 주웠다.",
        "그리고 이제 남은 날은 딱 5일. 마지막 닷새는 ‘시험의 날’이다.",
        "이제부터 나는, 지금까지 모은 쓰레기들을 제대로 분류하고 처리해서, 내 죄를 충분히 뉘우쳤음을 증명해내야 한다.",
        "만약 그러지 못하면……"
    };
    private List<string> opening3 = new List<string> { "그대로 지옥이지 뭐." };

    private List<List<string>> openings = new List<List<string>>();

    private int textIndex = 0;
    private int sceneIndex = 0;
    private bool canEnter = true;
    // Start is called before the first frame update
    void Start()
    {
        openings.Add(opening1);
        openings.Add(opening2);
        openings.Add(opening3);
        textBox = text.GetComponent<TextMeshProUGUI>();
        StartDialogue(textBox, openings[sceneIndex][textIndex], enter);
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
            
            if (textIndex == openings[sceneIndex].Count)
            {
                sceneIndex++;
                
                if(sceneIndex > 2)
                {
                    //end of opening scene
                    SoundManager.instance.AudioStop(SoundManager.instance.bgmSource);
                    GameManager.instance.StartGame();
                }
                else
                {
                    if (sceneIndex == 2)
                    {
                        SoundManager.instance.AudioStop(SoundManager.instance.bgmSource);
                        SoundManager.instance.AudioStart(SoundManager.instance.bgmSource, SoundManager.instance.badEnding_bgm);
                    }
                    background.GetComponent<Image>().sprite = Resources.Load<Sprite>("Art/CutScene/Opening/" + "OpeningIllust" + (sceneIndex+1).ToString());
                    textIndex = 0;
                    StartDialogue(textBox, openings[sceneIndex][textIndex], enter);
                }
            }
            else if (textIndex < openings[sceneIndex].Count)
            {
                StartDialogue(textBox, openings[sceneIndex][textIndex], enter);
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
