using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollector : MonoBehaviour
{
    public List<Trash> collected_right;
    public List<Trash> collected_wrong;
    public RunUI runUI;
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        collected_right = new List<Trash>();
        collected_wrong = new List<Trash>();
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
