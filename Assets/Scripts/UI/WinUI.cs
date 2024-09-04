using System.Collections;
using System.Collections.Generic;
using Localization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{

    [SerializeField] private GameObject _normalWinPanel;
    [SerializeField] private GameObject _achievementWinPanel;
    [SerializeField] private Text _title;
    [SerializeField] private Text _subtitle;
    [SerializeField] private Text _nextLevelText; //used when opening the end game panel
    [SerializeField] private Button _nextLevelButton;
    private int _mostAchieved;
    public void NextLevel()
    {
        if (GameRules.instance.currentS < SecondBrain.instance.LevelsInPack)
        {
            GameRules.instance.currentS++;
            SceneManager.LoadScene("e" + GameRules.instance.currentE + "s" + (GameRules.instance.currentS-1));
        }else
        {
            //InGameUI.instance.Menu();
            Back();
        }
    }

    public void Replay()
    {
       // Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Back()
    {
        SceneManager.LoadScene(0);
        // Application.LoadLevel(0);
    }

    // Use this for initialization
    void Start () {
        // Debug.LogWarning("WINSCREEEN");
        if(PlayerPrefs.GetInt("e" + GameRules.instance.currentE + "s" + (GameRules.instance.currentS - 1) + "completed", 0)==0){
            PlayerPrefs.SetInt("e" + GameRules.instance.currentE + "s" + (GameRules.instance.currentS - 1) + "completed", 1);
            _mostAchieved = PlayerPrefs.GetInt("e" + GameRules.instance.currentE + "completed", 0) + 1;
			PlayerPrefs.SetInt("e" + GameRules.instance.currentE + "completed", _mostAchieved);
            //OneSignalManager.instance.SendTag(GameRules.instance.currentE + "pack",  mostAchieved.ToString());
            //GPGSManager.instance.CheckPackCompletedAchievement(GameRules.instance.currentE);
            
        }
        CheckAchievement(GameRules.instance.currentE, _mostAchieved);


        // AdsManager.Instance.UpdateAds();

        //if(++SecondBrain.instance.AdsReferenceGameCount % SecondBrain.instance.ActualPopUpRate == 0)
        //{

        //    TedrasoftSocial.SocialSystem.Instance.RatePopUp();
        //   // PlayerPrefs.SetInt("AskReviewShows",PlayerPrefs.GetInt("AskReviewShows",0)+1);
        //   // PlayerPrefs.SetInt("LastPopUpRate",SecondBrain.instance.SecondPopUpRate);
        //    PlayerPrefs.Save();
        //  //  SecondBrain.instance.ActualPopUpRate = PlayerPrefs.GetInt("LastPopUpRate",SecondBrain.instance.SecondPopUpRate);
        //   // SecondBrain.instance.AdsReferenceGameCount = 0;
        //}
    }

    private void CheckAchievement(int episode, int levelsCompleted)
    {
        
        
            switch (episode)
            {
                case 0:
                    if (!GameRules.instance.FinishedEasyPack && levelsCompleted == 100 && GameRules.instance.FinishedMediumPack && GameRules.instance.FinishedHardPack)
                    {
                        _title.text = LanguageManager.Get("complete");
                        _subtitle.text = LanguageManager.Get("gameComplete");
                        _nextLevelText.text = LanguageManager.Get("mainmenu");
                        GameRules.instance.FinishedEasyPack = true;
                        PlayerPrefs.SetInt("EasyPack", 1);
                        _nextLevelButton.onClick.RemoveAllListeners();
                        _nextLevelButton.onClick.AddListener(()=>ToMainMenuUI.Instance.ToMainMenu() );
                        _achievementWinPanel.SetActive(true);
                    }
                    else if (!GameRules.instance.FinishedEasyPack && levelsCompleted == 100)
                    {
                        _title.text = LanguageManager.Get("congrats");
                        _subtitle.text = LanguageManager.Get("easyPackComplete");
                        _nextLevelText.text = LanguageManager.Get("nextlevel");
                        GameRules.instance.FinishedEasyPack = true;
                        PlayerPrefs.SetInt("EasyPack", 1);
                        _achievementWinPanel.SetActive(true);
                    }
                    else if (levelsCompleted<100)
                    {
                        _normalWinPanel.SetActive(true);
                    }
                    
                    break;
                case 1:
                    if (!GameRules.instance.FinishedMediumPack && levelsCompleted == 100 && GameRules.instance.FinishedEasyPack && GameRules.instance.FinishedHardPack)
                    {
                        _title.text = LanguageManager.Get("complete");
                        _subtitle.text = LanguageManager.Get("gameComplete");
                        _nextLevelText.text = LanguageManager.Get("mainmenu");
                        GameRules.instance.FinishedMediumPack = true;
                        PlayerPrefs.SetInt("MediumPack", 1);
                        _nextLevelButton.onClick.RemoveAllListeners();
                        _nextLevelButton.onClick.AddListener(() => ToMainMenuUI.Instance.ToMainMenu());
                        _achievementWinPanel.SetActive(true);
                    }
                    else if (!GameRules.instance.FinishedMediumPack && levelsCompleted == 100)
                    {
                        _title.text = LanguageManager.Get("congrats");
                        _subtitle.text = LanguageManager.Get("mediumPackComplete");
                        _nextLevelText.text = LanguageManager.Get("nextlevel");
                        GameRules.instance.FinishedMediumPack = true;
                        PlayerPrefs.SetInt("MediumPack", 1);
                        _achievementWinPanel.SetActive(true);
                    }
                    else if (levelsCompleted < 100)
                    {
                        _normalWinPanel.SetActive(true);
                    }

                break;
                case 2:
                    if (!GameRules.instance.FinishedHardPack && levelsCompleted == 100 && GameRules.instance.FinishedEasyPack && GameRules.instance.FinishedMediumPack)
                    {
                        _title.text = LanguageManager.Get("complete");
                        _subtitle.text = LanguageManager.Get("gameComplete");
                        _nextLevelText.text = LanguageManager.Get("mainmenu");
                        GameRules.instance.FinishedHardPack = true;
                        PlayerPrefs.SetInt("HardPack", 1);
                        _nextLevelButton.onClick.RemoveAllListeners();
                        _nextLevelButton.onClick.AddListener(() => ToMainMenuUI.Instance.ToMainMenu());
                        _achievementWinPanel.SetActive(true);
                    }
                    else if (!GameRules.instance.FinishedHardPack && levelsCompleted == 100)
                    {
                        _title.text = LanguageManager.Get("congrats");
                        _subtitle.text = LanguageManager.Get("hardPackComplete");
                        _nextLevelText.text = LanguageManager.Get("nextlevel");
                        GameRules.instance.FinishedHardPack = true;
                        PlayerPrefs.SetInt("HardPack", 1);
                        _achievementWinPanel.SetActive(true);
                    }
                    else if (levelsCompleted < 100)
                    {
                        _normalWinPanel.SetActive(true);
                    }

                break;
            }

        
    }

}