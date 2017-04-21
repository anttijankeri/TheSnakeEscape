using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Switches the scene to the corresponding
/// int in the build summary list.
/// </summary>
public class LoadSceneOnClick : MonoBehaviour {

	public void LoadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
