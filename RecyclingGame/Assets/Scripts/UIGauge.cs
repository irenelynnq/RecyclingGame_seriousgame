using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGauge : MonoBehaviour
{
    public Image guageRect;
    public Image guageFill;
    public Camera camera;
    private Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = camera.WorldToScreenPoint(target.position);

        guageRect.transform.position = screenPos;
        guageFill.transform.position = screenPos;
    }
}
