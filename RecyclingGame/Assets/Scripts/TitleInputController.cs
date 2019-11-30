using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleInputController : MonoBehaviour
{
    public GameObject menuPointer;

    enum TitleMenu
    {
        GameStart,
        GameExit
    }

    TitleMenu titleMenu;
    // Start is called before the first frame update
    void Start()
    {
        titleMenu = TitleMenu.GameStart;
        SoundManager.instance.AudioStart(SoundManager.instance.bgmSource, SoundManager.instance.opening_bgm);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            SoundManager.instance.FxSound(SoundManager.instance.cursor_fx);
            switch (titleMenu)
            {
                case TitleMenu.GameStart:
                    titleMenu = TitleMenu.GameExit;
                    menuPointer.GetComponent<Transform>().position = new Vector3(2f, -3f);
                    break;
                case TitleMenu.GameExit:
                    titleMenu = TitleMenu.GameStart;
                    menuPointer.GetComponent<Transform>().position = new Vector3(2f, -1.7f);
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            SoundManager.instance.AudioStop(SoundManager.instance.bgmSource);
            switch (titleMenu)
            {
                case TitleMenu.GameStart:
                    GameManager.instance.StartGame();
                    break;
                case TitleMenu.GameExit:
                    GameManager.instance.QuitGame();
                    break;
            }
        }
    }
}
