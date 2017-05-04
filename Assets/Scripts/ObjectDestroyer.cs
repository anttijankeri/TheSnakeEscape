using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// Destroys a Unity object with the specified name when walking into the trigger zone
/// </summary>
public class ObjectDestroyer : MonoBehaviour {
	// the object name of the object to be destroyed
	[SerializeField]
	private string objectName;

	// how long until the object is destroyed when the triggering happens
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
	
	// Update is called once per frame
	void Update () {
		// if the trigger has fired
		if (triggered)
		{
			// reduce time left
			triggerTimeGone += Time.deltaTime;

			// check if time is up
			if (triggerTimeGone >= triggerTimer)
			{
				// trigger the object destruction
				GameObject.Destroy (GameObject.Find (objectName));

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
