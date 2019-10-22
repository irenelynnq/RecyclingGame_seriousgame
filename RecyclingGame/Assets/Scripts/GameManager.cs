using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    
    public StageDB db;

    public GameObject run;
    public GameObject player;

    RunController runController;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        db = new StageDB();
        db.DBInit();
        runController = run.GetComponent<RunController>();
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //making stage
            MakeRunStage(1);
        }
    }
    void MakeRunStage(int level)
    {
        runController.InitRunStage(level, db.GetStageItem(level));
        //db.StageInit(db.GetStageItem(level), "TrashPosition_" + level.ToString());
    }
}
