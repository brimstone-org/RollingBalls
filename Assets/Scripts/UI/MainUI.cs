using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{

    public static MainUI Instance;

    public GameObject selectLevelGO;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            Instance = this;
        }
        else
        {
            Instance = this;
        }
    }

    public GameObject easySelectLevelGO;
    public void EasyGame()
    {
        easySelectLevelGO.SetActive(true);
        gameObject.SetActive(false);
    }

    public GameObject mediumSelectLevelGO;
    public void MediumGame()
    {
        mediumSelectLevelGO.SetActive(true);
        gameObject.SetActive(false);
    }

    public GameObject hardSelectLevelGO;
    public void HardGame()
    {
        hardSelectLevelGO.SetActive(true);
        gameObject.SetActive(false);
    }

    public GameObject settingsGO;
    public void Settings()
    {
        settingsGO.SetActive(true);
        gameObject.SetActive(false);
    }

    public GameObject QuitGO;
    public void Quit()
    {
        QuitGO.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Start(){
        if (SoundManager.SoundState != PlayerPrefs.GetInt("soundState", 1))
        {
            SoundManager.SoundState = PlayerPrefs.GetInt("soundState", 1);
            CheckSound();
        }
    }

    public Toggle soundToggle;
    public Image soundToggleImg;
    public Sprite soundToggleSpriteOn, soundToggleSpriteOff;
    public void SoundToggle()
    {
        SoundManager.SoundState = SoundManager.SoundState == 1 ? 0 : 1;
        PlayerPrefs.SetInt("soundState", SoundManager.SoundState);
        CheckSound();

    }

    void CheckSound(){
		if (SoundManager.SoundState == 1) soundToggleImg.sprite = soundToggleSpriteOn;
		else soundToggleImg.sprite = soundToggleSpriteOff;
    }

    void Update()
    {

#if UNITY_ANDROID
        // Make sure user is on Android platform
      //  if (Application.platform == RuntimePlatform.Android)
       // {

            // Check if Back was pressed this frame
            //if (Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Space))
            //{
            //    // Quit the application
            //    Quit();
            //}
      //  }
#endif
    }
   
}
