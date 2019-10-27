using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCollector : MonoBehaviour
{
    public List<Trash> collected_right;
    public List<Trash> collected_wrong;

    // Start is called before the first frame update
    void Start()
    {
        collected_right = new List<Trash>();
        collected_wrong = new List<Trash>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
