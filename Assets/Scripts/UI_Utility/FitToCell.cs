using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class FitToCell : MonoBehaviour
{
#if UNITY_EDITOR
    void Update()
    {
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, transform.parent.parent.GetComponent<GridLayoutGroup>().cellSize.x);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, transform.parent.parent.GetComponent<GridLayoutGroup>().cellSize.y);
    }
#endif
}
