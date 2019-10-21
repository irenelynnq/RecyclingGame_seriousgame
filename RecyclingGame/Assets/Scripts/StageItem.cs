using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageItem : MonoBehaviour
{
    public int stage_number;
    public string name;
    public string preprocess;
    
    public int pass_criteria;
    public bool is_passed;

    public int trashCount;
    public int answerCount;

    public float trashStartingPoint;
    public float trashGap;
    public List<Trash> trashList;
    public List<Trash> answerTrashList;
}
