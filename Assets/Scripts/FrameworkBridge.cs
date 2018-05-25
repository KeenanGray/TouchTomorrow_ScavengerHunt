using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using AOT;
using TheNextFlow.UnityPlugins;
using Touch_Tomorrow;

    public class FrameworkBridge : MonoBehaviour
    {
        public static GameObject tape;
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void framework_init();

    [DllImport("__Internal")]
    private static extern void framework_beginNFCSession();

    [DllImport("__Internal")]
    private static extern void framework_message(string message);

    [DllImport("__Internal")]
    private static extern void framework_setDelegate(DelegateMessage callback);

    [DllImport("__Internal")]
    private static extern void framework_setDialogueDelegate(DelegateDialogue callback);

    [DllImport("__Internal")]
    private static extern void framework_dialoguePrompt();

    private delegate void DelegateDialogue(bool response);
    private delegate void DelegateMessage(string nfcMessage);

    [MonoPInvokeCallback(typeof(DelegateMessage))]
    private static void delegateMessageReceived(string nfcMessage)
    {
        Debug.Log("Message received: " + nfcMessage);
        tape.GetComponent<UI_Controller>().CreateTickerTapeMessage(nfcMessage);
    }
    [MonoPInvokeCallback(typeof(DelegateMessage))]
    private static void delegateDialoguePromptAnswered(bool response)
    {
        Debug.Log("Response received: " + response);
        if (response)
            wipeSavedData();
        else
            Debug.Log("Response was no");
    }
#endif

#if UNITY_ANDROID

#endif

        public void Start()
        {
            tape = GameObject.FindWithTag("MainCanvas");
            if (tape != null)
            {
            }
        }

    public static void initNFCReader()
    {
#if UNITY_IOS
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            framework_init();
        }
#endif
    }

        public static void beginNFCSession()
        {
#if UNITY_IOS
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            framework_beginNFCSession();
        }
#endif
        }

        public static void message(string message)
        {
#if UNITY_IOS
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            framework_message(message);
        }
#endif
        }

        public static void initializeDelegate()
        {
#if UNITY_IOS
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            framework_setDelegate(delegateMessageReceived);
        }
#endif
        }

        public static void clearSavedData()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_OSX
        wipeSavedData();
#endif
#if UNITY_IOS
        //trigger iOS Native prompt
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            framework_setDialogueDelegate(delegateDialoguePromptAnswered);
            framework_dialoguePrompt();
        }
#endif

#if UNITY_ANDROID
       // AndroidNativePopups.
        MobileNativePopups.OpenAlertDialog(
            "Clear All Message Data?", "Warning:This will erase all previously scanned messages",
            "Continue", "Cancel",
                () =>
                {
                    wipeSavedData();
                },
                () => {  });
            
#endif

        }

        static void wipeSavedData()
        {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("TextDisplay"))
        {
            Destroy(go);
            go.transform.parent.GetComponent<TickerTapeContainer>().isTicking = false;
        }
            TextFileUtility.CleanTextFile(Application.persistentDataPath + "/savedMessages.txt");
        }
    }
