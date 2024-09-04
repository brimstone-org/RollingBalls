using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRules : MonoBehaviour {

    static public GameRules instance;
    public int currentE, currentS;
    public float ballSpeed=1f,holeSpeed=1f;
    public bool redOn;
    public Quaternion accQuat = Quaternion.identity;
    public int controlBallsType = 0; //0=acc,1=touch,2=acc+touch

    public bool FinishedEasyPack;
    public bool FinishedMediumPack;
    public bool FinishedHardPack;
    /*
    public struct Level
    {
        public int progress;
    }*/
    /*
    public struct Episode
    {
        public int progress;
        public Level[] levels;
    }*/

    public int episodes;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ReadProgress()
    {
      /*  string str = PlayerPrefs.GetString("Progress", "3*0:0:0:0:0:0:0:0:0:0:0:0*0:0:0:0:0:0:0:0:0:0:0:0*0:0:0:0:0:0:0:0:0:0:0:0");
        string[] strs1 = str.Split('*');*/

        //int episodesNum = int.Parse(strs1[0]);
        int episodesNum = SecondBrain.instance.Packs;

        //episodes = new Episode[episodesNum];
        episodes = SecondBrain.instance.LevelsInPack;

        /*

        for( int k=0; k<episodes.Length; k++ )
        {
            string[] strs2 = strs1[k + 1].Split(":"[0]);
            int numLevels = strs2.Length;
            episodes[k].levels = new Level[numLevels];
            for( int i=0; i<numLevels; i++ )
            {
                episodes[k].levels[i].progress = int.Parse(strs2[i]);
            }
        }*/
    }

    public void SaveProgress()
    {
       /* string str = "";
        str += "" + episodes.Length;
        for( int k=0; k<episodes.Length; k++ )
        {
            str += "*";
            for( int i=0; i<episodes[k].levels.Length; i++ )
            {
                if (i != 0) str += ":";
                str += "" + episodes[k].levels[i].progress;
            }
        }
        PlayerPrefs.SetString("Progress", str);
        PlayerPrefs.Save();*/
        PlayerPrefs.SetInt("LastLevelAchievedInPack" + currentE, currentS);
    }

  /*  public void ScoreWin(){
    //    PlayerPrefs.SetInt("e"+currentE+"s"+currentS,1);

    }*/

    public int LastAchievedLevelInPack(int pack)
    {
        return PlayerPrefs.GetInt("LastLevelAchievedInPack" + pack, 0);
    }

    // Use this for initialization
    void Start () {
        ReadProgress();
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        //get info about achievements
        //below if is hardcoding for testing
       // PlayerPrefs.DeleteAll();
        //for (int i = 0; i < 100; i++)
        //{
        //    PlayerPrefs.SetInt("e" + 0 + "s" + i + "completed", 1);
        //    PlayerPrefs.SetInt("e" + 1 + "s" + i + "completed", 1);
        //}
        //for (int i = 1; i < 100; i++)
        //{
        //    PlayerPrefs.SetInt("e" + 2 + "s" + i + "completed", 1);
        //   // PlayerPrefs.SetInt("e" + 1 + "s" + i + "completed", 1);
        //}
        //PlayerPrefs.SetInt("e" + 0 + "completed", 100);
        //PlayerPrefs.SetInt("e" + 1 + "completed", 100);
        //PlayerPrefs.SetInt("e" + 2 + "completed", 99);


        if (!PlayerPrefs.HasKey("EasyPick"))
        {
            PlayerPrefs.SetInt("EasyPack", 0);
        }
        if (PlayerPrefs.GetInt("EasyPack")==1)
        {
            FinishedEasyPack = true;
        }
        if (!PlayerPrefs.HasKey("MediumPick"))
        {
            PlayerPrefs.SetInt("MediumPack", 0);
        }
        if (PlayerPrefs.GetInt("MediumPack") == 1)
        {
            FinishedMediumPack = true;
        }
        if (!PlayerPrefs.HasKey("HardPick"))
        {
            PlayerPrefs.SetInt("HardPick", 0);
        }
        if (PlayerPrefs.GetInt("HardPick") == 1)
        {
            FinishedHardPack = true;
        }
       
    }

    private void OnApplicationFocus(bool pause)
    {
        if (!pause && InGameUI.instance!=null)
        {
            InGameUI.instance.Menu();
        }
    }
	
}
