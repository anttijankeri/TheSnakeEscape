using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// Triggers the NPC to start moving on their path when the player collides
/// </summary>
public class NPCTrigger : MonoBehaviour {
	// the object name of the npc to trigger
	[SerializeField]
	private string triggeredObjectName;

	// timer for how long until the npc starts moving after the trigger fires
	[SerializeField]
	private float triggerTimer = 0;

	// whether the trigger should wait until the room teleporter is open == all puzzles solved
	[SerializeField]
	private bool waitUntilOpen = false;

	// dialogue trigger name
	[SerializeField]
	private string dialogueTriggerName = "";

	// if the trigger has been triggered
	private bool triggered = false;

	// time elapsed since triggering
	private float triggerTimeGone = 0;

	/// <summary>
	/// When triggering with the player
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter (Collider other)
	{
		// check if the trigger is the player
		if (other.gameObject.name == "SnakeHead")
		{
			// exit if all puzzles not yet solved and need to wait for it
			if (waitUntilOpen)
			{
				if (!GameObject.Find("RoomTeleporter").GetComponent<RoomTeleporter> ().Open)
				{
					return;
				}
			}

			// start the trigger
			triggered = true;
		}
	}

	void Update ()
	{
		// if the trigger has fired
		if (triggered)
		{
			// reduce time left
			triggerTimeGone += Time.deltaTime;

			// check if time is up
			if (triggerTimeGone >= triggerTimer)
			{
				// trigger the npc's movement
				GameObject.Find (triggeredObjectName).GetComponent<NPCPathing> ().moving = true;

				// trigger some dialogue if necessary
				if (dialogueTriggerName != "")
				{
					DialogueTrigger.TriggerDialogue (dialogueTriggerName);
				}

				// self destruct
				GameObject.Destroy (gameObject);
			}
		}
	}
}