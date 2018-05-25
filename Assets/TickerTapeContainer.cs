using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Touch_Tomorrow
{
    struct TextData
    {
        public string character;
        public bool isPartOfText;


        public TextData(string a, bool b)
        {
            character = a;
            isPartOfText = b;
        }
    }
    public struct TapeData
    {
        public bool include;
        public bool instant;
        public string text;

        public TapeData(string a, bool b)
        {
            text = a;
            include = b;
            instant = false;
        }
    }

    public class TickerTapeContainer : MonoBehaviour
    {
        public Vector2 ScreenWidthAndHeight;

        public bool isTicking;
        GameObject DisplayPrefab;
        string pathToSavedMessages;
        private string[] savedMessages;

        // Use this for initialization
        void Start()
        {
            isTicking = false;
            DisplayPrefab = Resources.Load("TickerDisplay") as GameObject;

            pathToSavedMessages = Application.persistentDataPath + "/savedMessages.txt";
            Debug.Log(pathToSavedMessages);

            ScreenWidthAndHeight = GetComponent<RectTransform>().rect.size;
            GetComponent<RectTransform>().sizeDelta = new Vector2(ScreenWidthAndHeight.x, ScreenWidthAndHeight.y);

            savedMessages = TextFileUtility.ReadString(pathToSavedMessages).Split('\n');
        }

        // Update is called once per frame
        void Update()
        {
        }

        IEnumerator CreateTickerTape(string msg){
            GameObject display;
            if (!isTicking)
            {
                TextFileUtility.WriteString(pathToSavedMessages, msg);

                isTicking = true;
                display = Instantiate(DisplayPrefab, gameObject.transform);
                display.transform.SetAsFirstSibling();

                display.GetComponent<TickerDisplay>().SetWidthAndHeight(ScreenWidthAndHeight);

                yield return new WaitForFixedUpdate();
                yield return display.GetComponent<TickerDisplay>().StartCoroutine("TickerTheTape", new TapeData(msg, true));

                isTicking = false;


            }
            yield break;
        }

        IEnumerator LoadSavedMessages(){
            GameObject display;
            foreach (string s in savedMessages)
            {
                if (s != "")
                {
                    display = Instantiate(DisplayPrefab, gameObject.transform);
                    display.transform.SetAsFirstSibling();
                    display.GetComponent<TickerDisplay>().SetWidthAndHeight(ScreenWidthAndHeight);

                    TapeData instantData = new TapeData(s, true);
                    instantData.instant = true;

                    yield return new WaitForFixedUpdate();
                    display.GetComponent<TickerDisplay>().StartCoroutine("TickerTheTape", instantData);
                    isTicking = false;
                }
            }
            yield break;
        }
    }


}
