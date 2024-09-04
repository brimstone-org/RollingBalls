using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalibrateUI : MonoBehaviour {

    public Image img;
    public Text calibText;
    //Quaternion cal;

    public void Calibrate()
    {
        StartCoroutine(CalibrateDone());
    }

    IEnumerator CalibrateDone()
    {
        calibText.text = "CALIBRATION...";
        Vector3 calibInput = Input.acceleration;
        for( int k=0; k<60; k++ )
        {
            calibInput = Vector3.Lerp(calibInput, Input.acceleration, 1f/(float)(k+2));
            yield return new WaitForEndOfFrame();
        }
        GameRules.instance.accQuat = Quaternion.Inverse(Quaternion.FromToRotation(new Vector3(0f, 0f, -1f), calibInput));
        string str = calibText.text;
        calibText.text = "CALIBRATION DONE!";
        yield return new WaitForSeconds(1f);
        calibText.text = str;
    }

    public GameObject backGO;
    public void Back()
    {
        PlayerPrefs.SetString("AccQuat", "" + GameRules.instance.accQuat.eulerAngles.x + ":" + GameRules.instance.accQuat.eulerAngles.y + ":" + GameRules.instance.accQuat.eulerAngles.z);
        PlayerPrefs.Save();
        backGO.SetActive(true);
        gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
		
	}


	
	// Update is called once per frame
	void Update () {
        Quaternion cal = Quaternion.FromToRotation(GameRules.instance.accQuat * Input.acceleration, new Vector3(0f, 0f, -1f));
        img.rectTransform.localPosition = new Vector3(-5f*Mathf.DeltaAngle(cal.eulerAngles.y,0f), 5f*Mathf.DeltaAngle(cal.eulerAngles.x,0f), 0f);
	}

    public GUISkin skin;

    void OnGUI()
    {
        GUI.skin = skin;
        //GUI.Box(new Rect(0f, 0f, 500f, 100f), "" + GameRules.instance.accQuat*Input.acceleration*100f);
        //GUI.Box(new Rect(0f, 100f, 500f, 100f), "" + Input.acceleration * 100f);
        //GUI.Box(new Rect(0f, 100f, 500f, 100f), "" + cal.eulerAngles);
    }

}
