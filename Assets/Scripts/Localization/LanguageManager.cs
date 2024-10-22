﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Localization
{

    public class LanguageManager : MonoBehaviour
    {

        public static LanguageManager Instance { get; private set; }

        [Header("Options")]
        [SerializeField]
        [Tooltip("Used if <<UseDeviceLanguage>> is unchecked")]
        private SystemLanguage defaultLanguage = SystemLanguage.English;
        public SystemLanguage Language = SystemLanguage.English;
        private string languageTag = "";
        public string LanguageTag { get { return languageTag; } }
        [SerializeField]
        private bool useDeviceLanguage = false;
        [SerializeField]
        SystemLanguage[] supportedLanguages = { SystemLanguage.English };

        private Dictionary<string, string> fields;
        private Dictionary<SystemLanguage, string> languageTags;

        [Header("Fonts")]
        [SerializeField]
        private Font defaultFont;
        [SerializeField]
        private List<FontCategory> specialFonts;

        [Header("Resources")]
        [SerializeField]
        private string fileLocation = "Languages/values";

        [Header("Debugging")]
        [SerializeField]
        private bool debugging = false;
        [SerializeField]
        private string debuggingTag = "_dbg";

        void Awake()
        {
            //PlayerPrefs.DeleteAll();
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else if (Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            Language = defaultLanguage;

            fields = new Dictionary<string, string>();

            languageTags = new Dictionary<SystemLanguage, string>();
            for (int i = 0; i < supportedLanguages.Length; i++)
            {
#if UNITY_EDITOR
                if (Resources.Load(fileLocation + LanguagePair.GetTag(supportedLanguages[i])) == null)
                    Debug.LogError("LANGUAGE MANAGER: Language File Missing: " + supportedLanguages[i]);
#endif
                languageTags[supportedLanguages[i]] = LanguagePair.GetTag(supportedLanguages[i]);
            }

            Debug.Log("LanguageManager: device Language: " + Application.systemLanguage.ToString());



            if (useDeviceLanguage)
                Language = Application.systemLanguage;
            if (languageTags.ContainsKey(Language))
                languageTag = languageTags[Language];
            else
                languageTag = LanguagePair.GetTag(defaultLanguage);

            if (debugging)
                languageTag = debuggingTag;
            if (PlayerPrefs.HasKey("Language"))
            {
                SetLanguage(PlayerPrefs.GetString("Language"));
                return;
            }
            LoadLanguage(languageTag);
        }

        public void SetLanguage(string langTag)
        {
            if (debugging)
                return;
            foreach (var lang in languageTags)
            {
                if (langTag == lang.Value)
                {
                    this.Language = lang.Key;
                    languageTag = languageTags[lang.Key];
                    LoadLanguage(languageTag);
                    break;
                }
            }
        }

        private void LoadLanguage(string lang)
        {
            fields.Clear();
            TextAsset textAsset = (TextAsset)Resources.Load(fileLocation + languageTag);

            if (textAsset == null)
            {
                throw new Exceptions.LanguageFileMissing(lang);
            }
            string allTexts = string.Empty;
            allTexts = textAsset.text;
            string[] lines = allTexts.Split(new string[] { "\r\n", "\n" },
                System.StringSplitOptions.None);
            string key, value;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("#"))
                    continue;
                if (lines[i].IndexOf("=") >= 0 && !lines[i].StartsWith("#"))
                {
                    key = lines[i].Substring(0, lines[i].IndexOf("="));
                    value = lines[i].Substring(lines[i].IndexOf("=") + 1,
                        lines[i].Length - lines[i].IndexOf("=") - 1).Replace("\\n", System.Environment.NewLine);
                    fields.Add(key, value);
                }
            }
        }

        public static string Get(string key)
        {
            if (Instance == null)
                throw new Exceptions.NoInstance();

            string result;
            if (Instance.fields.TryGetValue(key, out result))
                return result;

            throw new Exceptions.NoKeyFound(key);
        }

        public static Font GetFont()
        {
            if (Instance == null)
                throw new Exceptions.NoInstance();

            for (int i = 0; i < Instance.specialFonts.Count; i++)
            {
                if (Instance.specialFonts[i].languages.Contains(Instance.defaultLanguage))
                    return Instance.specialFonts[i].font;
            }
            return Instance.defaultFont;
        }

    }
}
