using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour {
    public GameObject robotSprite;

    private void OnEnable()
    {
        robotSprite.SetActive(false);
    }

    private void OnDisable()
    {
        robotSprite.SetActive(true);
    }
}
