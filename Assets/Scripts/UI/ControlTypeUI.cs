using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlTypeUI : MonoBehaviour {

    public Toggle tilt,swipe,tiltSwipe;

    public void Toggled()
    {
        if (tilt.isOn) GameRules.instance.controlBallsType = 0;
        if (swipe.isOn) GameRules.instance.controlBallsType = 1;
        if (tiltSwipe.isOn) GameRules.instance.controlBallsType = 2;
    }

    public GameObject backGO;
    public void Back()
    {
        PlayerPrefs.SetInt("ControlType", GameRules.instance.controlBallsType);
        PlayerPrefs.SetString("ControlTypeStats", GameRules.instance.controlBallsType == 0 ? "tilt" : "swipe");
        PlayerPrefs.Save();

       // OneSignalManager.instance.SendTag("ControlType",PlayerPrefs.GetString("ControlTypeStats"));
        backGO.SetActive(true);
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        int control = PlayerPrefs.GetInt("ControlType", 0);
        if( control==0 )
        {
            tilt.isOn = true;
            swipe.isOn = false;
            tiltSwipe.isOn = false;
        }
        else if (control == 1)
        {
            tilt.isOn = false;
            swipe.isOn = true;
            tiltSwipe.isOn = false;
        }
        else if (control == 2)
        {
            tilt.isOn = false;
            swipe.isOn = false;
            tiltSwipe.isOn = true;
        }
    }

}
