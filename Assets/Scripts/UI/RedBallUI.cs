using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedBallUI : MonoBehaviour {

    public Toggle onoff;

    public void Toggled()
    {
        if (onoff.isOn) GameRules.instance.redOn = false;
        else GameRules.instance.redOn = true;
    }

    public GameObject backGO;
    public void Back()
    {
        PlayerPrefs.SetInt("RedOn", GameRules.instance.redOn ? 1 : 0);
        PlayerPrefs.SetInt("AllowRedBeforeWhiteBalls", GameRules.instance.redOn ? 1 : 0);
        PlayerPrefs.Save();

		//OneSignalManager.instance.SendTag("AllowRedBeforeWhiteBalls", PlayerPrefs.GetInt("AllowRedBeforeWhiteBalls", 0).ToString());

        backGO.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        int redOn = PlayerPrefs.GetInt("RedOn", 1);
        if( redOn==1 )
        {
            onoff.isOn = false;
            GameRules.instance.redOn = true;
        }
        if (redOn == 0)
        {
            onoff.isOn = true;
            GameRules.instance.redOn = false;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
