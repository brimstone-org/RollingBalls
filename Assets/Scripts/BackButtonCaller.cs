using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The actuall backbutton caller class that should sit on each element it should trigger.
/// </summary>
public class BackButtonCaller : MonoBehaviour
{

    /// <summary>
    /// The button we should press, if you leave this empty the application will quit.
    /// </summary>
    [SerializeField]
    Button targetBUtton;

    /// <summary>
    /// When the Game Object is enabled it changes the button.
    /// </summary>
   /* void OnEnable(){
       
    }*/

    void Start()
    {
#if UNITY_ANDROID
        targetBUtton = GetComponent<Button>(); }

    void OnEnable(){
        if (BackButtonHandler.controller == null)
            return;
//        Debug.Log("BUtton changed");
		targetBUtton = GetComponent<Button>();
        if (targetBUtton)
            BackButtonHandler.controller.ChangeButton(targetBUtton);
        else
        BackButtonHandler.controller.ChangeButton(null);

#endif
    }

    /// <summary>
    /// When the Game Object is disabled it restores the last target from the history stack.
    /// </summary>
    void OnDisable()
    {
     //   BackButtonHandler.controller.Restore();
    }

}