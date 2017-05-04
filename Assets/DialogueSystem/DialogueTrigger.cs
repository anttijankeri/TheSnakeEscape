using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// Creates an instant dialogue window when triggered
/// </summary>
public class DialogueTrigger : MonoBehaviour {
	// the actual dialogue manager
	private dialogueManager dMAn;

	public dialogueManager dman
	{
		get
		{
			return dMAn;
		}
	}

	// list of dialogue lines
	public string[] dialogueLines;

	/// <summary>
	/// Loads dialogueManager in the start of every scene
	/// </summary>
	// Use this for initialization
	void Start () {
		dMAn = FindObjectOfType<dialogueManager>();
	}

	/// <summary>
	/// Finds a dialogue trigger by name and triggers it
	/// </summary>
	/// <param name="triggerName">Trigger name.</param>
	public static void TriggerDialogue (string triggerName)
	{
		// find the right trigger
		DialogueTrigger trigger = GameObject.Find (triggerName).GetComponent<DialogueTrigger> ();

		// check if there was an actual trigger
		if (trigger != null)
		{
			trigger.dman.dialogLines = trigger.dialogueLines;
			trigger.dman.currentLine = 0;
			trigger.dman.ShowDialogue ();

			Destroy (trigger.gameObject);
		}
	}
}