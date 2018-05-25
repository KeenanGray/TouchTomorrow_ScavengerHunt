using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Touch_Tomorrow;

public class UI_Controller : MonoBehaviour {

    public GameObject SplashScreen;
    public GameObject DescriptionScreen;
    public GameObject TickerTapeScreen;
    public GameObject TickerTapeContainer;


    // Use this for initialization
	void Start () {
        SplashScreen.SetActive(true);
        StartCoroutine("ShowSplashScreen");
        DescriptionScreen.SetActive(false);

        FrameworkBridge.initializeDelegate();
        FrameworkBridge.initNFCReader();
	}

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        //Debug print messages from editor
        if (Input.GetKeyDown(KeyCode.K))
        {
            CreateTickerTapeMessage("Hello World! This is a test message in the editor.");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            CreateTickerTapeMessage("Twice as long message for testing. I hope I can get this doen by 2:00 AM. I suck at programming. I hate this.");
        }
#endif
    }

    public void CreateTickerTapeMessage(string message){
        TickerTapeContainer.GetComponent<TickerTapeContainer>().StartCoroutine("CreateTickerTape",message);
        TickerTapeContainer.GetComponent<TickerTapeContainer>().ScreenWidthAndHeight =
                               GetComponent<RectTransform>().sizeDelta;
    }

    public void OnScanButtonPressed()
    {
        if(!TickerTapeContainer.GetComponent<TickerTapeContainer>().isTicking)
            FrameworkBridge.beginNFCSession();
    }

    IEnumerator ShowSplashScreen()
    {
        yield return TickerTapeContainer.GetComponent<TickerTapeContainer>().StartCoroutine("LoadSavedMessages");

        yield return new WaitForSeconds(3.0f);

        SplashScreen.SetActive(false);

        yield break;
    }
}
