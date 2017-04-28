using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Trigger for starting the countdown timer
/// </summary>
public class TimeTrigger : MonoBehaviour {

	/// <summary>
	/// Starts the timer if the player collides with it after the room teleporter is open
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.name == "SnakeHead")
		{
			if (GameObject.Find("RoomTeleporter").GetComponent<RoomTeleporter> ().Open)
			{
				GameController.StartTimer ();
			}
		}
	}
}
