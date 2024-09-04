﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreApps : MonoBehaviour {

    public void OnClick()
    {
#if UNITY_ANDROID && !UNITY_AMAZON
        Application.OpenURL(GameLinks.GoooglePlayStoreDeveloper);
#elif UNITY_AMAZON && UNITY_ANDROID
        Application.OpenURL(GameLinks.AmazonStoreDeveloper);
#else
        Application.OpenURL(GameLinks.AppleAppStoreDeveloper);
#endif
    }
}