using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Causes an explosion that destroys all enemies {within line of sight and range}
/// </summary>
public class Explosion {

	// create new explosion with radius and possible wallhack at position x
	public Explosion (float range, bool wallhack, Vector3 position)
	{
		// create list of all gameobjects in the scene
		GameObject[] objectList = GameObject.FindObjectsOfType (typeof(GameObject)) as GameObject[];

		// go thru all the objects in the list
		foreach (GameObject objectInList in objectList)
		{
			// check if there's an enemy component
			if (objectInList.GetComponent<EnemyNPC> ())
			{
				// check if within range
				if ((objectInList.transform.position - position).magnitude <= range)
				{
					// check if wallhax or visible (no line of sight blockers aka walls)
					if (wallhack || !Physics.Raycast (position, objectInList.transform.position, (objectInList.transform.position - position).magnitude, 1 << 8))
					{
						// all checks are clear
						// destroy given object
						GameObject.Destroy (objectInList);
					}
				}
			}
		}
	}
}