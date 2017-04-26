using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// NPC pathing that allows creating paths and then the NPC to follow them
/// </summary>
public class NPCPathing : MonoBehaviour {
	[SerializeField]
	// allows the NPC to move back to the start from the end waypoint and keep going
	private bool looping;

	// the speed at which the NPC moves
	[SerializeField]
	private float speed;

	// a list containing all the waypoints, waypoints are relative to current position
	// the first waypoint MUST ALWAYS BE 0, 0, 0
	[SerializeField]
	private List<Waypoint> waypointList;

	// if the NPC should keep moving or not
	private bool finishedMoving = false;

	// the last waypoint the NPC touched
	private int currentWaypoint = 0;

	/// <summary>
	/// Draws the waypoints to help with placing them
	/// </summary>
	private void OnDrawGizmosSelected ()
	{
		// if no waypoints dont do anything
		if (waypointList == null)
		{
			return;
		}

		// loop thru all the waypoints and draw a line from the waypoint to the previous one
		for (int i = 1; i < waypointList.Count; i++)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawLine (waypointList [i].position + transform.position, waypointList [i - 1].position + transform.position);
		}

		// draw a line between the last and first waypoints if looping
		if (looping)
		{
			Gizmos.color = Color.blue;
			Gizmos.DrawLine (waypointList [0].position + transform.position, waypointList [waypointList.Count - 1].position + transform.position);
		}
	}

	// Update is called once per frame
	void Update () {
		// check if theres waypoints to move to
		if (waypointList != null && !finishedMoving)
		{
			// get the speed for this frame
			float frameSpeed = speed * Time.deltaTime;

			// keep moving until the speed for this frame is exhausted
			// (if the speed is really high or the waypoints are really close, we may need to move past multiple waypoints)
			while (frameSpeed > 0)
			{
				// get the next waypoint (either the first waypoint if at the last, or just the next waypoint)
				Vector3 nextWPPos = (currentWaypoint + 1 == waypointList.Count) ? waypointList [0].position : waypointList [currentWaypoint + 1].position;

				// calculate the positional difference between the current and targeted spots
				Vector3 posDifference = nextWPPos - transform.position;

				// get the distance to move towards the goal position
				float thisSpeed = Mathf.Min(frameSpeed, posDifference.magnitude);

				// move towards the goal
				transform.position += frameSpeed * posDifference.normalized;
			}
		}
	}
}