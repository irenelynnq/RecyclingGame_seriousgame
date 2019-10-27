using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreatInputController : MonoBehaviour
{
    public int selectPosition;
    public Vector3 selectOffset;
    public GameObject selectBox;
    // Start is called before the first frame update
    void Start()
    {
        selectPosition = 1;
        selectOffset = new Vector3(0, 1.22f);
        GameManager.instance.MakeTreatStage();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            bool isDone = TreatController.instance.currentStage.treatTrashDict[selectPosition].IsTreatDone();
            if (isDone) TreatController.instance.GotAnswer(selectPosition);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            switch (selectPosition)
            {
                case 5:
                case 10:
                    break;
                default:
                    selectPosition += 1;
                    selectBox.transform.position = TreatController.instance.treatPosition[selectPosition] + selectOffset;
                    break;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            switch (selectPosition)
            {
                case 1:
                case 6:
                    break;
                default:
                    selectPosition -= 1;
                    selectBox.transform.position = TreatController.instance.treatPosition[selectPosition] + selectOffset;
                    break;
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            switch (selectPosition)
            {
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                    break;
                default:
                    selectPosition += 5;
                    selectBox.transform.position = TreatController.instance.treatPosition[selectPosition] + selectOffset;
                    break;
            }
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            switch (selectPosition)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                    break;
                default:
                    selectPosition -= 5;
                    selectBox.transform.position = TreatController.instance.treatPosition[selectPosition] + selectOffset;
                    break;
            }
        }

        
    }


}
