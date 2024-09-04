using System.Collections;
using UnityEngine;

public class SettingsUI : MonoBehaviour {


    public GameObject ballColorsGO;
    public void BallsColors()
    {
        ballColorsGO.SetActive(true);
        gameObject.SetActive(false);
    }

    public GameObject redBallGO;
    public void RedBall()
    {
        redBallGO.SetActive(true);
        gameObject.SetActive(false);
    }

    public GameObject ballSpeedGO;
    public void BallSpeed()
    {
        ballSpeedGO.SetActive(true);
        gameObject.SetActive(false);
    }

    public GameObject holeSpeedGO;
    public void HoleSpeed()
    {
        holeSpeedGO.SetActive(true);
        gameObject.SetActive(false);
    }

    public GameObject controlTypeGO;
    public void ControlType()
    {
        controlTypeGO.SetActive(true);
        gameObject.SetActive(false);
    }

    public GameObject calibrateGO;
    public void Calibrate()
    {
        calibrateGO.SetActive(true);
        gameObject.SetActive(false);
    }

    public GameObject LanguageGO;
    public void Language()
    {
        LanguageGO.SetActive(true);
        gameObject.SetActive(false);
    }

    public GameObject backGO;
    public void Back()
    {
        backGO.SetActive(true);
        gameObject.SetActive(false);
    }

}
