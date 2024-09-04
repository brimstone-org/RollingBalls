using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkaroundFixForReplay : MonoBehaviour {

    public void Reload(){
        SceneController.instance.replay = true;
        Application.LoadLevel(Application.loadedLevel);
    }
}
