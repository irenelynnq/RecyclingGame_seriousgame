using System.Collections;
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

    //플레이어와 충돌시 게임화면에서 사라집니다
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "TrashCollector")
        {
            gameObject.SetActive(false);
            if (is_answer == true) gameObject.GetComponent<TrashCollector>().collected_right.Add(this);
            else gameObject.GetComponent<TrashCollector>().collected_wrong.Add(this);
        }
    }
}
