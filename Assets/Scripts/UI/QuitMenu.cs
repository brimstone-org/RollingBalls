using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitMenu : MonoBehaviour
{
    public void QuitApp()
    {
        Application.Quit();       
    }

    public GameObject backGO;
    public void Back()
    {
        backGO.SetActive(true);
        gameObject.SetActive(false);
    }
}
