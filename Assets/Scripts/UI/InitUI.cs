using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitUI : MonoBehaviour {

    public Slider ballSpeed, holeSpeed;

	// Use this for initialization
	void Start () {
        int orientation = PlayerPrefs.GetInt("Orientation", 0);
        if (orientation == 0) Screen.orientation = ScreenOrientation.Portrait;
        else Screen.orientation = ScreenOrientation.LandscapeLeft;
        ballSpeed.value = PlayerPrefs.GetFloat("BallSpeed", 1f);
        holeSpeed.value = PlayerPrefs.GetFloat("HoleSpeed", 1f);
        GameRules.instance.controlBallsType = PlayerPrefs.GetInt("ControlType", 0);

        int redOn = PlayerPrefs.GetInt("RedOn", 1);
        if (redOn == 1) GameRules.instance.redOn = true;
        else if (redOn == 0) GameRules.instance.redOn = false;
        

        string straq = PlayerPrefs.GetString("AccQuat", "0:0:0");
        string[] strsaq = straq.Split(":"[0]);
        GameRules.instance.accQuat = Quaternion.Euler(float.Parse(strsaq[0]), float.Parse(strsaq[1]), float.Parse(strsaq[2]));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
