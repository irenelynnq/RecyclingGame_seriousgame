﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageItem
{
    public int stage_number;
    public string name;
    public string preprocess;
    
    public int pass_criteria;
    public bool is_passed;

    public int trashCount;
    public int answerCount;

    public int treatTrashCount;
    public int treatAnswerCount;

    public float trashStartingPoint;
    public float trashGap;

    public List<Trash> trashList = new List<Trash>();
    public List<Trash> answerTrashList = new List<Trash>();

    public Dictionary<int, string> answerSpriteNameDict = new Dictionary<int, string>();

    public Dictionary<int, Trash> treatTrashDict = new Dictionary<int, Trash>();

    public void StageItemClean()
    {
        trashList = new List<Trash>();
        answerTrashList = new List<Trash>();
        answerSpriteNameDict = new Dictionary<int, string>();
        treatTrashDict = new Dictionary<int, Trash>();
    }
}
