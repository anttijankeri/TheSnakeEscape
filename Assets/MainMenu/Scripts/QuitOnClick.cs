using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Quits the application on button press
/// </summary>
public class QuitOnClick : MonoBehaviour {

	public void Quit()
    {
#if UNITY_EDITOR //If the current editor is in Unity, quits to the editor screen. Not entire unity.
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit ();
#endif
    }
}
