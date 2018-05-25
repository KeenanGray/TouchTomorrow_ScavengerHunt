using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetHeightBasedOnChild : MonoBehaviour {

    public GameObject child;
    RectTransform HeightRef;
    float offset = 200;

	// Use this for initialization
	void Start () {
        HeightRef = child.GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<RectTransform>().sizeDelta = new Vector2(HeightRef.sizeDelta.x, HeightRef.sizeDelta.y + offset);
	}
}
