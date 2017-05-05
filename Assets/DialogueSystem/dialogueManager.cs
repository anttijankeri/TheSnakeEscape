using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Shows and controls all the dialogue boxes
/// </summary>
public class dialogueManager : MonoBehaviour {
    //Dialogue box
    public GameObject dialogueBox;
    //Dialogue text
    public Text dialogueText;

	// if the dialogue window is currently active
    public static bool dialogueActive = false;

	// list of lines given by the dialogue holders
    public string[] dialogLines;
    public int currentLine;

	/// <summary>
	/// Advances the dialogue by one.
	/// </summary>
	public void AdvanceDialogue ()
	{
		// if dialogue is active
		if (dialogueManager.dialogueActive)
		{
			// advance line
			currentLine++;

			// Closes the conversation after the array ends
			if (currentLine >= dialogLines.Length)
			{
				//Closes dialogue boxes
				dialogueBox.SetActive(false);
				dialogueManager.dialogueActive = false;
			}
			// still text to show
			else
			{
				dialogueText.text = dialogLines[currentLine];
			}
		}
	}

	/// <summary>
	/// Turn off self-destruct on scene changes
	/// </summary>
	void Awake ()
	{
		// make the gameobject persistent
		DontDestroyOnLoad (transform.parent.gameObject);
	}

	// Use this for initialization
	void Start () {
		// add listener to dialogue button
		gameObject.transform.GetChild(0).GetChild(2).GetComponent<Button> ().onClick.AddListener (() => AdvanceDialogue());

		dialogueBox.SetActive(false);
	}

	/// <summary>
	/// Activates the dialogue box
	/// </summary>
    public void ShowDialogue()
    {
        dialogueManager.dialogueActive = true;
        dialogueBox.SetActive(true);
		dialogueText.text = dialogLines[currentLine];
    }
}