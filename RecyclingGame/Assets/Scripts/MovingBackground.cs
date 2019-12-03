using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    public float scrollSpeed;
    public Camera mainCam;
    private float startPos;
    private float camStartPos;

    public PlayerController player;

    private Material thisMaterial;

    // Start is called before the first frame update
    void Start()
    {
        thisMaterial = GetComponent<Renderer>().material;
        startPos = transform.position.x;
        camStartPos = mainCam.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.state == PlayerState.Running)
        {
            float dist = (mainCam.transform.position.x - camStartPos);
            transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

            Vector2 newOffset = thisMaterial.mainTextureOffset;
            newOffset.Set(newOffset.x + (scrollSpeed * Time.deltaTime), 0);
            thisMaterial.mainTextureOffset = newOffset;
        }
       
    }
}
