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

	/// <summary>
	/// When triggering with the player
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter (Collider other)
	{
		// check if the trigger is the player
		if (other.gameObject.name == "SnakeHead")
		{
			// trigger the npc's movement
			GameObject.Find (triggeredObjectName).GetComponent<NPCPathing> ().moving = true;

			// self destruct
			GameObject.Destroy (gameObject);
		}
	}
}