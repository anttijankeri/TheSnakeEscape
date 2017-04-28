using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// Contains a single set of dialogue that is sent to the manager when triggered
/// </summary>
public class dialogueHolder : MonoBehaviour {
	// the actual dialogue manager
    private dialogueManager dMAn;

	// if the holder should wait until the room teleporter is open => all puzzles solved
	[SerializeField]
	private bool waitUntilOpen = false;

	// list of dialogue lines
    public string[] dialogueLines;

	// if the dialogue has been triggered yet
	private bool dialogueUsed = false;

	// if the dialogue manager should repeat lines after the first lines have been shown => add to this array
	// the lines shown when repeating
	[SerializeField]
	private string[] repeatLines;

    /// <summary>
    /// Loads dialogueManager in the start of every scene
    /// </summary>
    // Use this for initialization
    void Start () {
        dMAn = FindObjectOfType<dialogueManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// If player collides with the text box collider,
    /// then show dialoguebox from dialogueManager
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
		// if the collider is the player
        if (other.gameObject.name == "SnakeHead")
        {
            //If dialogue is not active, give the manager its lines if necessary
			if (!dialogueManager.dialogueActive)
            {
				// exit if room teleporter isnt open and we're waiting for it to open
				if (waitUntilOpen)
				{
					if (!GameObject.Find("RoomTeleporter").GetComponent<RoomTeleporter> ().Open)
					{
						return;
					}
				}

				// if not shown anything from this holder yet
				if (!dialogueUsed)
				{
					dMAn.dialogLines = dialogueLines;
					dMAn.currentLine = 0;
					dMAn.ShowDialogue();
					dialogueUsed = true;
				}
				// if theres any repeatable lines to show
				else if (repeatLines.Length > 0)
				{
					dMAn.dialogLines = repeatLines;
					dMAn.currentLine = 0;
					dMAn.ShowDialogue();
				}
            }
        }
    }
}