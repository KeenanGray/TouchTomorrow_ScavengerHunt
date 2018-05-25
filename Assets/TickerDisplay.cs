using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Touch_Tomorrow
{

    public class TickerDisplay : MonoBehaviour
    {
        [TextArea]
        public string TextForDisplay;
        public int spacerNum;
        public int PaddedRowNum;
        public GameObject TextGrid;

        GameObject[] TextObjects;
        GameObject[] BlankTextObjects;
        GameObject tText;

        int lineCount;
        int totalLines;

        int tickCount;
        int ticksInCurrentRow;
        int BlankCount = 0;

        float TickDelay = 0.05f;
        float ribbonDelay = 0.075f;
        float ribbonAnimDelay = 0.2f;

        bool[] PaddedRows;

        private void Awake()
        {
            tText = Resources.Load("TickerText") as GameObject;

            PaddedRows = new bool[PaddedRowNum];
            for (int i = 0; i < PaddedRows.Length; i++)
                PaddedRows[i] = false;
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void SetText(string msg)
        {
            TextForDisplay = msg;
            FormatText();

            TextObjects = new GameObject[TextForDisplay.Length];
            BlankTextObjects = new GameObject[spacerNum * PaddedRowNum];
        }

        public void SetWidthAndHeight(Vector2 newWAndH){
            GetComponent<RectTransform>().sizeDelta = newWAndH;
            TextGrid.GetComponent<RectTransform>().sizeDelta = newWAndH;
        }

        void FormatText()
        {
            int lineLength;
            lineLength = TextGrid.GetComponent<DynamicGrid>().cellsPerRow;

            //Seperate the string into words
            string[] words = TextForDisplay.Split(' ');
            TextForDisplay = "";
            string current = "";

            current = words[0];
            //for each other word
            for (int i = 1; i < words.Length; i++)
            {
                //if current is longer than line
                if (current.Length > lineLength)
                {
                    Debug.Log("WORD SIMPLY WON'T FIT " + current);
                }

                //Get the next word in sequence
                //try to add the next word
                if ((current + " " + words[i]).Length < lineLength)
                {
                    //update current string to combined words
                    current += " " + words[i];
                }
                else
                {
                    //current word is fit to line as best as possible.
                    //Right pad the word with spaces
                    current = current.PadRight(lineLength, ' ');
                    //add padded word to text sequence for printing
                    TextForDisplay += current;

                    //increment the counter
                    current = words[i];
                    totalLines++;
                }

                if (current.Length == lineLength)
                {
                    //Debug.Log("current is exact " + current);
                }

                if (i + 1 >= words.Length)
                {
                    //Right pad the word with spaces
                    //add padded word to text sequence for printing
                    TextForDisplay += current;
                    //Debug.Log("last line added " + current);

                    totalLines++;
                }
            }
                for (int i = 0; i < TextForDisplay.Length % lineLength; i++)
                {
                    TextForDisplay += ' ';
                }
        }

        void AddTick(string character, bool include)
        {
            GameObject tmp;
            ticksInCurrentRow++;
            tmp = Instantiate(tText, TextGrid.transform);

            tmp.GetComponent<TextObject>().SetText(character);

            if (include)
            {
                if (TextObjects != null)
                {
                    TextObjects[tickCount] = tmp;
                    tickCount++;
                }
            }
            if (!include)
            {
                tmp.GetComponent<TextObject>().SetVisible(false);
                if (character == "\\ig")
                {
                    BlankTextObjects[BlankCount] = tmp;
                    BlankCount++;
                }
            }

        }

        IEnumerator TickerTheTape(TapeData data)
        {
            WaitForSeconds delay = new WaitForSeconds(TickDelay);

            SetText(data.text);

            for (int i = 0; i < TextForDisplay.Length; i++)
            {
                //Add spaces if robot is covering
                AddStartRowSpaces();

                AddTick(TextForDisplay[i].ToString(), data.include);

                //update the text of each object so it pushes backwards
                for (int j = 0; j < tickCount; j++)
                {
                    if (j < TextObjects.Length)
                    {
                        int count = 0;
                        for (int activeTape = j; activeTape >= 0; activeTape--)
                        {
                            TextObjects[activeTape].GetComponent<TextObject>().SetText(TextForDisplay[count].ToString());
                            count++;
                        }
                    }
                }

                //Check if text is at edge of screen
                if (ticksInCurrentRow == TextGrid.GetComponent<DynamicGrid>().cellsPerRow && tickCount > 0)
                {
                    if (!data.instant)
                        yield return new WaitForSeconds(ribbonDelay);

                    //Add ribbon row
                    ticksInCurrentRow = 0;

                    yield return StartCoroutine(AddRibbonLayer(data));
                }
                if (!data.instant)
                    yield return delay;
            }
            AddEndRowSpaces();
        
            yield break;
        }

        int StartCount = 0;
        void AddStartRowSpaces()
        {
            if (lineCount < PaddedRows.Length)
            {
                if (!PaddedRows[lineCount])
                {
                    for (int i = 0; i < spacerNum; i++)
                    {
                        AddTick("\\ig", false);
                        StartCount++;
                    }
                    PaddedRows[lineCount] = true;
                }
            }
        }

        //Adds spaces at the end of a ticker tape message in order to push the messsage upwards, away from the robot
        void AddEndRowSpaces()
        {
            //Update the ribbons that are visible so that they match to the correct line.
            for (int i = 0; i < TextObjects.Length - 1; i++)
            {
                if (TextObjects[i] != null)
                if (TextObjects[i].GetComponent<TextObject>().ribbonVisible)
                    {
                    foreach (TickerRibbon tr in TextObjects[i].GetComponent<TextObject>().GetTickerRibbons())
                        {
                            tr.visible = false;
                            tr.MakeInvisible();
                        }

                        int nextShiftedRibbon = i + spacerNum;

                        if (nextShiftedRibbon < TextObjects.Length)
                        {
                            if (TextObjects[nextShiftedRibbon] != null)
                            {
                            foreach (TickerRibbon tr in TextObjects[nextShiftedRibbon].GetComponent<TextObject>().GetTickerRibbons())
                                {
                                    tr.visible = true;
                                    tr.MakeVisible();
                                }
                                i += spacerNum;
                            }
                        }
                    }
            }

            //Add a bunch of blank ticks to push row upwards.
            for (int i = StartCount; i < TextGrid.GetComponent<DynamicGrid>().cellsPerRow * 2; i++)
            {
                AddEndTick();
            }
        }

        void AddEndTick()
        {
            GameObject tmp;
            ticksInCurrentRow++;
            tmp = Instantiate(tText, TextGrid.transform);

            tmp.transform.SetAsFirstSibling();
            foreach (GameObject go in BlankTextObjects)
            {
                if (go != null)
                    go.transform.SetAsFirstSibling();
            }
            tmp.GetComponent<TextObject>().SetText("\\ig");
            tmp.GetComponent<TextObject>().SetVisible(true);

        }


        //Coroutines
        IEnumerator AddRibbonLayer(TapeData td)
        {
            int ribbonLines = 0;

#if UNITY_IOS
            ribbonLines = totalLines;
#endif
            ribbonLines = totalLines - 1;

            if (lineCount < ribbonLines)
            {
                foreach (TickerRibbon tr in TextObjects[tickCount - 1].GetComponent<TextObject>().GetTickerRibbons())
                {
                    tr.MakeVisible();
                    tr.visible = true;

                    if (!td.instant)
                        yield return new WaitForSeconds(ribbonAnimDelay);
                }
            }
            lineCount++;

            yield break;
        }
    }
}
