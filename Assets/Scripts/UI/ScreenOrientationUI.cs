using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenOrientationUI : MonoBehaviour {

    public Toggle vertical, horizontal;

    private void OnEnable()
    {
        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            vertical.isOn = true;
            horizontal.isOn = false;
        }
        else
        {
            horizontal.isOn = true;
            vertical.isOn = false;
        }
    }

    public void Toggled()
    {
        if( vertical.isOn )
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }
        else Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    public GameObject backGO;
    public void Back()
    {
        PlayerPrefs.SetInt("Orientation", vertical.isOn ? 0 : 1);
        PlayerPrefs.Save();
        backGO.SetActive(true);
        gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
