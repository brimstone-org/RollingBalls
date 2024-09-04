using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallSpeedUI : MonoBehaviour {

    public Slider slider;
    public void OnSlider()
    {
        GameRules.instance.ballSpeed = slider.value;
    }

    public GameObject backGO;
    public void Back()
    {
        PlayerPrefs.SetFloat("BallSpeed", slider.value);
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
