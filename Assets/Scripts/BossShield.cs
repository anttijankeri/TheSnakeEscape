using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// Turns off enemy's unkillable when all objects in the
/// unity editor's list are destroyed/gone.
/// </summary>
public class BossShield : MonoBehaviour {
	// array containing all the objects' names to look of
	[SerializeField]
	private string[] objectNameList;

	// dialogue trigger name
	[SerializeField]
	private string dialogueTriggerName = "";

	// update frequency, how many seconds between each check
	[SerializeField]
	private float updateFrequency = 1;

	// time remaining until next check
	private float timeLeftToUpdate = 0;

	// Update is called once per frame
	void Update () {
		// check if need to update
		if (timeLeftToUpdate <= 0)
		{
			// go thru the object list
			foreach (string objectName in objectNameList)
			{
				// check if the object exists
				if (GameObject.Find (objectName) != null)
				{
					// object found, shield still active
					timeLeftToUpdate = updateFrequency;
					return;
				}
			}

			// no objects found -> shield should be removed
			GetComponent<EnemyNPC> ().unkillable = false;

			// trigger some dialogue if necessary
			if (dialogueTriggerName != "")
			{
				DialogueTrigger.TriggerDialogue (dialogueTriggerName);
			}

			// destroy component
			Destroy (this);
		}
		else
		{
			timeLeftToUpdate -= Time.deltaTime;
		}
	}
}