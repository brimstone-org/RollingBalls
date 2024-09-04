using UnityEngine;

public class DEBUGSCRIPT : MonoBehaviour
{
    public static DEBUGSCRIPT instance { get; set; }
    void Awake(){
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start() { DontDestroyOnLoad(this);

        PlayerPrefs.SetInt("e0completed", 99);
        PlayerPrefs.SetInt("e0s99completed", 0);

		PlayerPrefs.SetInt("e1completed", 99);
		PlayerPrefs.SetInt("e1s99completed", 0);

		PlayerPrefs.SetInt("e2completed", 99);
		PlayerPrefs.SetInt("e2s99completed", 0);
    }
}
