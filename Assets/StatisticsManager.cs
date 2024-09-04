using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsManager : MonoBehaviour {
    public static StatisticsManager instance { get; set; }


    void Awake() {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
        
        if (PlayerPrefs.GetInt("AllowRedBeforeWhiteBalls", -1) == -1)
        {
            PlayerPrefs.SetInt("AllowRedBeforeWhiteBalls", GameRules.instance.redOn ? 1 : 0);
            PlayerPrefs.Save();
            //OneSignalManager.instance.SendTag("AllowRedBeforeWhiteBalls",PlayerPrefs.GetInt("AllowRedBeforeWhiteBalls",0).ToString());
        }

        if (PlayerPrefs.GetInt("AdsShown",-1)==-1)
        {
            PlayerPrefs.SetInt("AdsShown",0);
            PlayerPrefs.Save();
            //OneSignalManager.instance.SendTag("AdsShown", PlayerPrefs.GetInt("AdsShown", 0).ToString());
        }

        if (PlayerPrefs.GetString("ControlTypeStats","none").Equals("none"))
        {
            PlayerPrefs.SetString("ControlTypeStats","tilt");
            PlayerPrefs.Save();
           // OneSignalManager.instance.SendTag("ControlType",PlayerPrefs.GetString("ControlTypeStats", "none"));
        }

    }

}
