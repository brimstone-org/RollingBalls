
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Class handling back button presses on Android.
/// </summary>
public class BackButtonHandler : MonoBehaviour
{
    /// <summary>
    /// Singleton design.
    /// </summary>
    public static BackButtonHandler controller { get; set; }

    [SerializeField]
    /// <summary>
    /// The button we should trigger the onclicks for if it's assigned.
    /// </summary>
    Button targetButton;

    /// <summary>
    /// Here we hold all the previous buttons so we can chain screens easily.
    /// </summary>
   // Stack<Button> historyBtns = new Stack<Button>();

    [SerializeField]
    /// <summary>
    /// Should the backbutton be activable right now?
    /// </summary>
    bool isEnabled = true;

    /// <summary>
    /// Init singleton design and push a null into the history.
    /// When it's null, pressing the backbutton will kill the app. We push the null since we want to kill the app in the mainscreen.
    /// </summary>
    void Awake()
    {
        if (controller == null)
            controller = this;
        else
            Destroy(this);
        DontDestroyOnLoad(gameObject);

        // historyBtns.Push(null);
    }
#if UNITY_ANDROID
    /// <summary>
    /// Checks input and conditions. If there's a button reference we call it's onclick, else we do a quit.
    /// </summary>
    void Update()
    {

        if ((Input.GetKeyUp("escape") || Input.GetKeyUp(KeyCode.Space) )&& isEnabled)
        {
            if (!targetButton)
                //Application.Quit();
                MainUI.Instance.Quit();
            else
            {
              //  targetButton.gameObject.SetActive(true);
				isEnabled = false;
				Invoke("SetEnabled", 0.4f);
                targetButton.onClick.Invoke();

            }


           
        }

}
#endif
	/// <summary>
	/// Assigns a new button we push the old one into the stack.
	/// </summary>
	/// <param name="btn">Button.</param>
	public void ChangeButton(Button btn)
    {
        //Debug.Log("TargetButtonChanged");
        //   historyBtns.Push(targetButton);

#if UNITY_ANDROID

        targetButton = btn;
        isEnabled = true;
# endif
    }

    /// <summary>
    /// Restores the top of the stack as the new target button, if it's not empty.
    /// </summary>
 /*   public void Restore()
    {
        if (historyBtns.Count > 0)
            targetButton = historyBtns.Pop();
        else
            targetButton = null;
    }*/

    /// <summary>
    /// Makes the backbutton actionable.
    /// </summary>
    public void SetEnabled()
    { isEnabled = true; }

}
