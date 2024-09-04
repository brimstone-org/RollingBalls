using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondBrain : MonoBehaviour
{
    public static SecondBrain instance { get; set; }

	public int Packs = 3;
	public int LevelsInPack = 100;
	public int AdsReferenceGameCount = 0;
	public int ActualPopUpRate=0;
	public int FirstPopUpRate = 10;
	public int SecondPopUpRate = 20;
	public float BallRatioToReality = 1.0f;
	public float BallOutAnimDuration = 0.5f;
	public int BallsLimit = 60;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

#if UNITY_IOS
        Application.targetFrameRate = 60;
#endif

      //  ActualPopUpRate = PlayerPrefs.GetInt("LastPopUpRate", FirstPopUpRate);
    }


   
}
