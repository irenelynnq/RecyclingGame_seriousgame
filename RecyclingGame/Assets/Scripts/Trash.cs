using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public string id;
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
            if (is_answer == true) collision.gameObject.GetComponent<TrashCollector>().collected_right.Add(this);
            else collision.gameObject.GetComponent<TrashCollector>().collected_wrong.Add(this);
        }
    }

    public int GotHit()
    {
        hitcount = Mathf.Min(hitcount + 1, 10);
        return hitcount;
    }
    public bool IsTreatDone()
    {
        hitcount = Mathf.Min(hitcount + 1, 10);
        if (hitcount == 10 && need_preprocess && !treatDone)
        {
            treatDone = true;
            return true;
        }
        else return false;
    }

    public void CopyInfo(Trash other)
    {
        this.id = other.id;
        this.name = other.name;
        this.is_answer = other.is_answer;
        this.need_preprocess = other.need_preprocess;
        this.sprite_name = other.sprite_name;
        this.yPosition = other.yPosition;
        this.xPosition = other.xPosition;
    }

}
