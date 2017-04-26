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

	// if the enemy NPC regains eyesight after finishing path
	[SerializeField]
	private bool regainEyesight;

	// if the NPC should keep moving or not
	[SerializeField]
	public bool moving;

	// should the NPC self destruct after finishing the path
	[SerializeField]
	private bool selfDestruct;

	// a list containing all the waypoints, waypoints are relative to current position
	// the first waypoint MUST ALWAYS BE 0, 0, 0
	[SerializeField]
	private List<Waypoint> waypointList;

	// the last waypoint the NPC touched
	private int currentWaypoint = 0;

	// the positional difference between current pos and the next waypoint
	private Vector3 posDifference;

	// the starting position of the object, used for calculating local pos differences
	private Vector3 startPos;

	/// <summary>
	/// Draws the waypoints to help with placing them
	/// </summary>
	private void OnDrawGizmosSelected ()
	{
		// if no waypoints dont do anything
		if (waypointList.Count == 0)
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

	/// <summary>
	/// Gets the next waypoint's coordinates and stuff
	/// </summary>
	private void UpdateNextWaypoint ()
	{
		// get the next waypoint (either the first waypoint if at the last, or just the next waypoint)
		Vector3 nextWPPos = (currentWaypoint + 1 == waypointList.Count) ? waypointList [0].position : waypointList [currentWaypoint + 1].position;

		// calculate the positional difference between the current and targeted spots
		posDifference = nextWPPos - (transform.position - startPos);
	}

	void Start ()
	{
		if (waypointList.Count > 0)
		{
			startPos = transform.position;

			UpdateNextWaypoint ();
		}
	}

	// Update is called once per frame
	void Update () {
		// if not paused
		if (!GameController.gamePaused) {
			// check if theres waypoints to move to
			if (waypointList.Count > 0 && moving) {
				// get the speed for this frame
				float frameSpeed = speed * Time.deltaTime;

				// keep moving until the speed for this frame is exhausted
				// (if the speed is really high or the waypoints are really close, we may need to move past multiple waypoints)
				while (frameSpeed > 0) {
					// get the distance to move towards the goal position
					float thisSpeed = Mathf.Min (frameSpeed, posDifference.magnitude);

					// move towards the goal
					transform.position += thisSpeed * posDifference.normalized;

					// change to the next waypoint if moved past it
					if (posDifference.magnitude <= frameSpeed)
					{
						currentWaypoint++;

						// if at the end of the path
						if (currentWaypoint == waypointList.Count) {
							currentWaypoint = 0;
						}
						// no looping, at the end
						else if (currentWaypoint + 1 == waypointList.Count)
						{
							// self destruct if necessary
							if (selfDestruct)
							{
								GameObject.Destroy (gameObject);
							}

							moving = false;

							// regain eyesight to chase enemies
							if (GetComponent<EnemyNPC> () && regainEyesight)
							{
								GetComponent<EnemyNPC> ().blind = false;
							}

							return;
						}

						UpdateNextWaypoint ();
					}
					// not moved past waypoint, reduce distance left
					else {
							// reduce the position difference total
							posDifference -= thisSpeed * posDifference.normalized;
					}

					// reduce the remaining speed
					frameSpeed -= thisSpeed;
				}
			}
		}
	}
}