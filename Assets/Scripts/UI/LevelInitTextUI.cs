using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInitTextUI : MonoBehaviour {

    public string initText = "";

    public GameObject initTextUI, mainUI;
    public Text initTextText;

	// Use this for initialization
	void Start () {
		if( initText!="" )
        {
            initTextText.text = Localization.LanguageManager.Get(initText);
            initTextUI.SetActive(true);
            mainUI.SetActive(false);
            Time.timeScale = 0f;
        }
	}

    public void StartGame()
    {
        mainUI.SetActive(true);
        initTextUI.SetActive(false);
        Time.timeScale = 1f;
    }

}
