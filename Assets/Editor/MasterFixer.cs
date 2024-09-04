using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using UnityEngine.SceneManagement;
using UnityEditorInternal;
using NUnit.Framework.Internal.Commands;
using System.Runtime.CompilerServices;


public class MasterFixer : EditorWindow
{
    //Use these to easily edit your levels.
    #region EDITOR UTILITY METHODS

    struct PROGRESS_BAR
    {
        public string title;
        public string info;
        public int size;
    };

    static PROGRESS_BAR PBAR;

    static void UpdateProgressBar(int val)
    {
        EditorUtility.DisplayProgressBar(PBAR.title, PBAR.info, (float)Mathf.Clamp(val, 0, PBAR.size) / (float)PBAR.size);
    }

    static void EndProgressBar()
    {
        EditorUtility.ClearProgressBar();
        PBAR.info = null;
        PBAR.title = null;
        PBAR.size = 0;
    }

    static void BeginProgressBar(string title, string info, int size)
    {
        if (PBAR.title == null)
            PBAR.title = title;
        if (PBAR.info == null)
            PBAR.info = info;
        if (PBAR.size == 0)
            PBAR.size = size;
    }

    static void SetProgressBar(string title = null, string info = null, int size = 0)
    {
        PBAR.title = title;
        PBAR.info = info;
        PBAR.size = size;
    }

    /// <summary>
    /// Runs the passed method on all objects found with the name given in all the scenes included in build (checked).
    /// WARNING: It cannot find disabled game objects. Work around this by finding something else and parsing the child,
    /// In the future there should be an alternative method to get anything except DontDestroyOnLoadObjects
    /// </summary>
    /// <param name="name">Name of the object.</param>
    /// <param name="Method">Method.</param>
	static void RunMethodForAllObjectsWithNameInAllLevels(string name, Action<GameObject> Method)
    {

        try
        {
            if (Method == null)
                return;


            List<string> allLevels = new List<string>();
            allLevels = GetAllScenesInBuild();

            List<GameObject> hierarchyGOs;

            BeginProgressBar("Scenes Progress", "Executing task on each scene", allLevels.Count - 1);
            int IDX = 0;

            foreach (string levelName in allLevels)
            {
                UpdateProgressBar(IDX++);
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(levelName);
                hierarchyGOs = new List<GameObject>(GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[]);


                foreach (GameObject GO in hierarchyGOs)
                {
                    if (GO.name == name)
                        Method(GO);
                }

            }
            EndProgressBar();
        }
        catch (System.Exception e)
        {
            EndProgressBar();
            Debug.LogError(e.Message);
            return;
        }

    }


    /// <summary>
    /// Finds even disabled objects but doesn't find DontDestroyOnLoad Objects. It is very expensive even in editor.
    /// </summary>
    /// <param name="name">Name.</param>
    /// <param name="Method">Method.</param>
    static void RunMethodForAllObjectsWithNameInAllLevelsRoot(string name, Action<GameObject> Method)
    {
        try
        {
            if (Method == null)
                return;


            List<string> allLevels = new List<string>();
            allLevels = GetAllScenesInBuild();
            GameObject foundObj;

            List<GameObject> hierarchyGOs = new List<GameObject>();

            BeginProgressBar("Scenes Progress", "Executing task on each scene", allLevels.Count - 1);
            int IDX = 0;

            foreach (string levelName in allLevels)
            {
                UpdateProgressBar(IDX++);
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene(levelName).GetRootGameObjects(hierarchyGOs);

                foreach (GameObject GO in hierarchyGOs)
                {
                    foundObj = FindFirstObjectWithName(name, GO);
                    if (foundObj != null)
                    {
                        Method(foundObj);
                        break;
                    }
                }

            }
            EndProgressBar();
        }
        catch (System.Exception e)
        {
            EndProgressBar();
            Debug.LogError(e.Message);
            return;
        }
    }


    /// <summary>
    /// Costly recursive method even in editor that searches in all childs of a parent object and in their respective childs for the first occurence.
    /// </summary>
    /// <returns>The first object with the given name, if exists.</returns>
    /// <param name="name">Name.</param>
    /// <param name="parent">The object in which childs we'll search.</param>
    static GameObject FindFirstObjectWithName(string name, GameObject parent)
    {
        int childs = parent.transform.childCount;
        if (childs > 0)
        {
            GameObject parsedObject;
            GameObject auxObject;
            for (int i = 0; i < childs; i++)
            {
                parsedObject = parent.transform.GetChild(i).gameObject;
                if (parsedObject.name.Contains(name))
                    return parsedObject;
                else
                    auxObject = FindFirstObjectWithName(name, parsedObject);

                if (auxObject != null)
                    return auxObject;
            }
        }
        return null;
    }

    static List<string> GetAllScenesInBuild()
    {
        List<string> levelScenes = new List<string>();

        levelScenes = new List<string>(UnityEditor.EditorBuildSettingsScene.GetActiveSceneList(UnityEditor.EditorBuildSettings.scenes));

        return levelScenes;
    }


    #endregion

    [MenuItem("Levels Fixer/Clear Additional Win Objects")]
    static void ClearAdditionalWinScreen()
    {
        SetProgressBar("Cleaning WinScreen Duplicates", "Duplicate Win GameObjects from level scenes are getting deleted.");

        Transform auxTransform = null;
        Transform auxDuplicate = null;
        UnityEngine.SceneManagement.Scene auxScene;

        RunMethodForAllObjectsWithNameInAllLevels("Canvas", (GOBJ) =>
        {
            bool marked = false;

            Debug.Log("Found Canvas");
            for (int i = 0; i < GOBJ.transform.childCount; i++)
            {

                auxTransform = GOBJ.transform.GetChild(i);
                if (auxTransform.gameObject.name.Contains("Win") && marked)
                {
                    Debug.Log("Found Win duplicate");
                    auxScene = auxTransform.gameObject.scene;
                    DestroyImmediate(auxDuplicate.gameObject);
                    UnityEditor.SceneManagement.EditorSceneManager.SaveScene(auxScene);
                    break;
                }

                if (auxTransform.gameObject.name.Contains("Win"))
                {
                    marked = true;
                    auxDuplicate = auxTransform;
                }
            }


        });

    }

    [MenuItem("Levels Fixer/Clear Init Text")]
    static void ClearInitText()
    {

        SetProgressBar("Cleaning InitText Object", "InitText objects from all levels are getting deleted.");

        UnityEngine.SceneManagement.Scene auxScene;

        RunMethodForAllObjectsWithNameInAllLevelsRoot("InitText", (GOBJ) =>
        {
            Debug.Log("Found InitText");
            auxScene = GOBJ.gameObject.scene;
            DestroyImmediate(GOBJ);
            UnityEditor.SceneManagement.EditorSceneManager.SaveScene(auxScene);
        });

    }

    [MenuItem("Levels Fixer/Attach BackButton handler to levels")]
	static void AttachBackButtonHandler()
	{

            SetProgressBar("Fixing BackButton", "Attaching BackButtonHandler to all levels.");


            Transform auxTransform = null;
            RunMethodForAllObjectsWithNameInAllLevels("Canvas", (GOBJ) =>
            {



                for (int i = 0; i < GOBJ.transform.childCount; i++)
                {

                    auxTransform = GOBJ.transform.GetChild(i);

                    if (auxTransform.name.Contains("InGame"))
                    {

                        Transform GO = auxTransform.GetChild(0);
                        Debug.Log("Found InGame");
                        if (GO.gameObject.GetComponent<BackButtonCaller>() == null)
                        {
                            GO.gameObject.AddComponent<BackButtonCaller>();

                        }


                    }
                    else if (auxTransform.name.Contains("Menu"))
                    {
                        Transform GO = auxTransform.GetChild(auxTransform.childCount - 1).transform;

                        Debug.Log("Found Button (7)");
                        if (GO.gameObject.GetComponent<BackButtonCaller>() == null)
                        {
                            GO.gameObject.AddComponent<BackButtonCaller>();
                            //     UnityEditor.SceneManagement.EditorSceneManager.SaveScene(auxTransform.gameObject.scene);
                        }
                    }
                    else if (auxTransform.name.Contains("Win"))
                    {
                        Transform GO = auxTransform.GetChild(auxTransform.childCount - 2).transform;

                        Debug.Log("Found Button (7)");
                        if (GO.gameObject.GetComponent<BackButtonCaller>() == null)
                        {
                            GO.gameObject.AddComponent<BackButtonCaller>();
                            //	UnityEditor.SceneManagement.EditorSceneManager.SaveScene(auxTransform.gameObject.scene);
                        }
                    }

                }
                UnityEditor.SceneManagement.EditorSceneManager.SaveScene(GOBJ.gameObject.scene);

            });


	}

    [MenuItem("Levels Fixer/Add Replay button")]
	static void AttachReplayButtonHandler()
	{
       

            SetProgressBar("Attaching Replay button", "Attaching Replay Button prefab to all levels.");


            RunMethodForAllObjectsWithNameInAllLevels("Canvas", (GOBJ) =>
            {

                for (int i = 0; i < GOBJ.transform.GetChild(1).childCount;i++)
            {
                    if (GOBJ.transform.GetChild(1).GetChild(i).gameObject.name.Contains("Replay"))
                    {
                        DestroyImmediate(GOBJ.transform.GetChild(1).GetChild(i).gameObject);
                        i = 0;
                    }
            }




                
               GameObject replayButton = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Replay.prefab");
                replayButton = Instantiate(replayButton);
                replayButton.transform.SetParent(GOBJ.transform.GetChild(1), false);
                replayButton.transform.SetSiblingIndex(replayButton.transform.parent.childCount - 2);

                 Debug.Log("Created Replay Button");

                UnityEditor.SceneManagement.EditorSceneManager.SaveScene(GOBJ.gameObject.scene);

            });


	}
}
