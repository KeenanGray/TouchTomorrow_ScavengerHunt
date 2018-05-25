using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Touch_Tomorrow
{
    public enum Sprite_Name
    {
        Sprite_Start,
        Sprite_Whole,
        Sprite_End
    }

#if UNITY_EDITOR
#endif

    public class TickerText : MonoBehaviour
    {
        private TextMeshProUGUI text;
        private Image sprite;

        public GameObject ribbonHolder;
        public bool ribbonVisible;

        public float FirstChildPosition;

        // Use this for initialization
        void Awake()
        {
            text = GetComponentInChildren<TextMeshProUGUI>();
            sprite = GetComponentInChildren<Image>();
            ribbonVisible = false;
        }

        public void SetText(string Letter)
        {
            if (text != null)
                text.text = Letter;
            else
                Debug.Log("text is null in go: " + gameObject.name);
        }

        public string GetText()
        {
            return text.text;
        }

        public void SetSprite(Sprite_Name name)
        {
            switch (name)
            {
                case Sprite_Name.Sprite_Start:
                    sprite.sprite = null;
                    break;
                case Sprite_Name.Sprite_Whole:
                    sprite.sprite = null;
                    break;
                case Sprite_Name.Sprite_End:
                    sprite.sprite = null;
                    break;
            }
        }

        public void SetVisible(bool myBool)
        {
            if (text.text != "\\ig")
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
            else{
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

            public TickerRibbon[] GetTickerRibbons()
            {           
                return ribbonHolder.GetComponentsInChildren<TickerRibbon>();
            }
        }

    
}