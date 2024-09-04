using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallColorsUI : MonoBehaviour {

    public Toggle red,blue,yellow,green;

    public void Toggled()
    {

    }

    public GameObject backGO;
    public void Back()
    {
        backGO.SetActive(true);
        gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
