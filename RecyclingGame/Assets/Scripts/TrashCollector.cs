using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollector : MonoBehaviour
{
    public List<int> collected_right;
    public List<int> collected_wrong;
    public RunUI runUI;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        collected_right = new List<int>();
        collected_wrong = new List<int>();
        playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotWrong()
    {
        playerController.Flicker();
        GameManager.instance.life -= 1;
        runUI.UpdateLife();
        if (GameManager.instance.life == 0) GameManager.instance.GameOver();

    }
}
