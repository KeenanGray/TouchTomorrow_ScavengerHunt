using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSavedData : MonoBehaviour {


    public void OnClearSaveButton()
    {
        FrameworkBridge.clearSavedData();
    }
}
