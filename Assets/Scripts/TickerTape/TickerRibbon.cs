using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Touch_Tomorrow
{
    public class TickerRibbon : MonoBehaviour
    {
        public GameObject[] RibbonPieces;
        Vector2 normalizedPosition;
        Vector3 initPos;

        GameObject MainCanvas;

        public float height = 0;
        public bool visible;

        private void Awake()
        {
            visible = false;
            MainCanvas = GameObject.FindWithTag("MainCanvas");
        }

        private void Start()
        {
            //StartCoroutine("PositionRibbon");

                float yOffset = 100;
            Vector2 referenceSize = MainCanvas.GetComponent<RectTransform>().sizeDelta;
            Vector2 referencePos = transform.parent.parent.parent.position;

            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2((referenceSize.x / 2.5f), 120);

            if (name == "Backward")
            {
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(referenceSize.x - (referenceSize.x / 2.5f), yOffset);
            }
            else if (name == "Forward")
            {
                gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(referenceSize.x - (referenceSize.x / 2.5f) - GetComponent<RectTransform>().sizeDelta.x, yOffset);
            }
            else
                Debug.LogWarning("This Script must be attached to a gameobject named 'Forward' or 'Backward' ");

        }

        private void Update()
        {

        }

        public void MakeVisible()
        {
            Color tmpColor = transform.GetComponentInChildren<Image>().color;
            transform.GetComponentInParent<TextObject>().ribbonVisible = true;

            foreach (Image im in transform.GetComponents<Image>())
                im.color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, 255);
        }

        public void MakeInvisible()
        {

            Color tmpColor = transform.GetComponentInChildren<Image>().color;
            transform.GetComponentInParent<TextObject>().ribbonVisible = false;

            foreach (Image im in transform.GetComponentsInChildren<Image>())
                im.color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, 0);
        }

    }
}