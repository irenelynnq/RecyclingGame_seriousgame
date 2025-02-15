﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public int id;
    public string name;
    public bool is_answer;
    public bool need_preprocess;
    public string sprite_name;

    public TrashPosition yPosition;
    public float xPosition;

    public int hitcount;
    public bool treatDone;

    //플레이어와 충돌시 게임화면에서 사라집니다
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "TrashCollector")
        {
            
            gameObject.SetActive(false);
            if (is_answer == true)
            {
                SoundManager.instance.FxSound(SoundManager.instance.get_fx);
                collision.gameObject.GetComponent<TrashCollector>().collected_right.Add(this.id);
            }
            else if (is_answer == false)
            {
                SoundManager.instance.WrongFx();
                collision.gameObject.GetComponent<TrashCollector>().collected_wrong.Add(this.id);
                collision.gameObject.GetComponent<TrashCollector>().GotWrong();
                Debug.Log("Got Wrong!!" + name + " " + xPosition.ToString());
            }
        }
    }

    public int GotHit()
    {
        hitcount = Mathf.Min(hitcount + 1, 15);
        return hitcount;
    }
    public bool IsTreatDone()
    {
        hitcount = Mathf.Min(hitcount + 1, 15);
        if (hitcount == 15 && need_preprocess && !treatDone)
        {
            treatDone = true;
            return true;
        }
        else return false;
    }
    public int GetHitCount()
    {
        return hitcount;
    }

    public void CopyInfoFrom(Trash other)
    {
        id = other.id;
        name = other.name;
        is_answer = other.is_answer;
        need_preprocess = other.need_preprocess;
        sprite_name = other.sprite_name;
        yPosition = other.yPosition;
        xPosition = other.xPosition;
    }

    public int CompareTo(Trash other)
    {
        return this.id.CompareTo(other.id);
    }
}


