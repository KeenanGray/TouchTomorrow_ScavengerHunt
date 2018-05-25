using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Touch_Tomorrow
{

    public class TextObject : MonoBehaviour
    {
        public TextMeshProUGUI myChar;
        public GameObject ribbonHolder;
        public bool ribbonVisible;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetText(string letter)
        {
            myChar.text = letter;
        }

        public TickerRibbon[] GetTickerRibbons()
        {
            return ribbonHolder.GetComponentsInChildren<TickerRibbon>();
        }

        public void SetVisible(bool myBool)
        {
            if (myChar.text != "\\ig")
            {
                int value = 255;
                if (myBool)
                    value = 0;

                for (int i = 0; i < transform.childCount; i++)
                {
                    Color tmpColor = transform.GetComponentInChildren<Image>().color;

                    foreach (Image im in transform.GetComponentsInChildren<Image>())
                    {
                        im.color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, value);
                    }
                    tmpColor = transform.GetComponentInChildren<TextMeshProUGUI>().color;
                    transform.GetComponentInChildren<TextMeshProUGUI>().color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, value);
                }
            }
            else
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    Color tmpColor = transform.GetComponentInChildren<Image>().color;

                    foreach (Image im in transform.GetComponentsInChildren<Image>())
                    {
                        im.color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, 0);
                    }
                    tmpColor = transform.GetComponentInChildren<TextMeshProUGUI>().color;
                    transform.GetComponentInChildren<TextMeshProUGUI>().color = new Color(tmpColor.r, tmpColor.g, tmpColor.b, 0);
                }
            }
        }
    }
}
