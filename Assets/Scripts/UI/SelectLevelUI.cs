using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevelUI : MonoBehaviour {

    public int episode = 0;
    public Sprite levelOn, levelOff;
    public int buttons;
    public Button buttonReference;
    GameObject[] levelButtons;


    int last_level = 0;

    Button btnAux;
    Image imgAux;
    Text txtAux;
    GameObject doneAux;

    void OnEnable(){

        if (levelButtons!=null)
        {
            //int newEnable = GameRules.instance.LastAchievedLevelInPack(episode);
            for (int i = 0; i < SecondBrain.instance.LevelsInPack; i++)
            {

				btnAux = levelButtons[i].GetComponent<Button>();
				doneAux = btnAux.transform.GetChild(1).gameObject;
				doneAux.SetActive(false);

                if (PlayerPrefs.GetInt("e" + episode + "s" + i + "completed", 0) == 1)
                {
                    doneAux.SetActive(true);
                }
               
            }
        }
    }

    void Start()
    {
        buttons = SecondBrain.instance.LevelsInPack;

        levelButtons = new GameObject[buttons];

        int lastAchievedLevel = GameRules.instance.LastAchievedLevelInPack(episode);
       

        for (int i = 0; i < buttons;i++)
        {
            btnAux = Instantiate(buttonReference, Vector3.zero, Quaternion.identity, buttonReference.gameObject.transform.parent);
          
            imgAux = btnAux.GetComponent<Image>();
            txtAux = btnAux.GetComponentInChildren<Text>();
            doneAux = btnAux.transform.GetChild(1).gameObject;
            doneAux.SetActive(false);


                if (PlayerPrefs.GetInt("e" + episode + "s" + i + "completed", 0) == 1)
                    doneAux.SetActive(true);

                    
            int x = i + 1;
            btnAux.onClick.AddListener(()=>{

                StartLevel(x);
            });

            txtAux.text = (i + 1).ToString();

			btnAux.gameObject.SetActive(true);
            levelButtons[i] = btnAux.gameObject;
        }

    }

    public void StartLevel(int level)
    {
        last_level = GameRules.instance.currentS;
        GameRules.instance.currentE = episode;
        GameRules.instance.currentS = level;
        SceneManager.LoadScene("e" + episode + "s" + (level - 1));
        //Application.LoadLevel("e"+episode+"s"+(level-1));
        //Application.LoadLevel(1);
    }

    public GameObject backGO;
    public void Back()
    {
        backGO.SetActive(true);
        gameObject.SetActive(false);
    }

    void Clean(){
        
    }
}
