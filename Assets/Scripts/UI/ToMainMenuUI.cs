using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainMenuUI : MonoBehaviour
{

    public static ToMainMenuUI Instance;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            Instance = this;
        }
        else
        {
            Instance = this;
        }
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Replay(){
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public GameObject backGO;
    public void Back()
    {
        backGO.SetActive(true);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }


}
