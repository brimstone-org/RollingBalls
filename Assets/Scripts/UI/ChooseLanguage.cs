using System.Collections;
using System.Collections.Generic;
using Localization;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLanguage : MonoBehaviour
{
    [SerializeField] private Sprite _selectedLang;
    [SerializeField] private Sprite _unselectedLang;
   
    [SerializeField] private Image _englishButton;
    [SerializeField] private Image _germanButton;
    [SerializeField] private Image _spanishButton;
    [SerializeField] private Image _frenchButton;
    [SerializeField] private Image _italianButton;
    [SerializeField] private Image _portugueseButton;
    [SerializeField] private Image _russianButton;

    [SerializeField]List<TranslatedText> _listOfTextsInMenus = new List<TranslatedText>();

    void Start()
    {
        UpdateLanguageButtons(LanguagePair.GetTag(LanguageManager.Instance.Language));
    }


    public void SelectLanguage(string name)
    {
        UpdateLanguageButtons(name);
        LanguageManager.Instance.SetLanguage(name);
        PlayerPrefs.SetString("Language", name);
        for (int i = 0; i < _listOfTextsInMenus.Count; i++)
        {
            _listOfTextsInMenus[i].UpdateText();
        }
    }

    private void UpdateLanguageButtons(string language)
    {
        _englishButton.overrideSprite = _unselectedLang;
        _germanButton.overrideSprite = _unselectedLang;
        _spanishButton.overrideSprite = _unselectedLang;
        _frenchButton.overrideSprite = _unselectedLang;
        _italianButton.overrideSprite = _unselectedLang;
        _portugueseButton.overrideSprite = _unselectedLang;
        _russianButton.overrideSprite = _unselectedLang;
        switch (language)
        {
            case "":
                _englishButton.overrideSprite = _selectedLang;

                break;
            case "_de":
                _germanButton.overrideSprite = _selectedLang;
                break;
            case "_it":
                _italianButton.overrideSprite = _selectedLang;
                break;
            case "_fr":
                _frenchButton.overrideSprite = _selectedLang;
                break;
            case "_es":
                _spanishButton.overrideSprite = _selectedLang;
                break;
            case "_pt":
                _portugueseButton.overrideSprite = _selectedLang;
                break;
            case "_ru":
                _russianButton.overrideSprite = _selectedLang;
                break;
        }
      
    }

    public GameObject BackGO;
    public void Back()
    {
        //PlayerPrefs.SetInt("ControlType", GameRules.instance.controlBallsType);
        //PlayerPrefs.SetString("ControlTypeStats", GameRules.instance.controlBallsType == 0 ? "tilt" : "swipe");
        //PlayerPrefs.Save();

        // OneSignalManager.instance.SendTag("ControlType",PlayerPrefs.GetString("ControlTypeStats"));
        BackGO.SetActive(true);
        gameObject.SetActive(false);
    }
}
