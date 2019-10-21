using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public StageDB db;
    // Start is called before the first frame update
    void Start()
    {
        db = new StageDB();
        db.DBInit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
