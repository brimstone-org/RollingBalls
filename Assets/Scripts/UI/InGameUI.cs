using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour {

    public static InGameUI instance { get; set; }

    public GameObject menuGO;


    void Awake(){
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);
    }

    public void Menu()
    {
        menuGO.SetActive(true);
        gameObject.SetActive(false);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
