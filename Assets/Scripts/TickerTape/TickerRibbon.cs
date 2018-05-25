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

        public float height = 0;
        public bool visible;

        private void Awake()
        {
            visible = false;
        }

        private void Start()
        {
            //StartCoroutine("PositionRibbon");
        }

        private void Update()
        {

        }

        IEnumerator PositionRibbon()
        {
            WaitForSeconds delay = new WaitForSeconds(0.25f);
            int cnt = 0;

            while (true)
            {
                if (cnt < 10)
                    cnt++;

                float yOffset = 93;
                Vector2 referenceSize = transform.parent.parent.parent.gameObject.GetComponent<RectTransform>().sizeDelta;
                Vector2 referencePos = transform.parent.parent.parent.position;

                gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2((referenceSize.x / 2.5f), 100);

                if (name == "Backward")
                {
                    gameObject.GetComponentInParent<TickerText>().FirstChildPosition = referenceSize.x - (referenceSize.x / 2.5f);
                    gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(gameObject.GetComponentInParent<TickerText>().FirstChildPosition, yOffset);
                }
                else if (name == "Forward")
                {
                    gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(gameObject.GetComponentInParent<TickerText>().FirstChildPosition - referenceSize.x / 2.5f, yOffset);
                }
                else
                    Debug.LogWarning("This Script must be attached to a gameobject named 'Forward' or 'Backward' ");

                if (cnt > 10)
                    yield return delay;
            }
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