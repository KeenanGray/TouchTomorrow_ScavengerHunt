using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Touch_Tomorrow;

public class DynamicGrid : MonoBehaviour
{
    float width;
    float height;
    GameObject TopLevelCanvas;

    public int cellsPerRow;

    private void Start()
    {
        //TopLevelCanvas = GameObject.FindWithTag("MainCanvas");

        //width = TopLevelCanvas.GetComponent<RectTransform>().rect.width;
        //height = TopLevelCanvas.GetComponent<RectTransform>().rect.height;

        //GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        //GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);   
          
        float numerator = GetComponent<RectTransform>().sizeDelta.x;
        float denominator = GetComponent<GridLayoutGroup>().cellSize.x;

        cellsPerRow = Mathf.FloorToInt(numerator / denominator);
    }

    //public void SetActiveUI(bool myBool){
    //    foreach(TickerText tt in GetComponentsInChildren<TickerText>()){
    //        tt.SetVisible(!myBool);
    //    }
    //}
}
