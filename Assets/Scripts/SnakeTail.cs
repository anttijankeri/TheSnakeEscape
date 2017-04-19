using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Individual snake tail section's script for controlling the positioning and rotation
/// </summary>
public class SnakeTail : MonoBehaviour {
	// the gameobject this tail section is following
	private GameObject followedObject;

	// the sequential number this section is (1 through infinity)
	private int tailNumber;

	// the snakehead's script containing speed values
	private SnakeHead snakeHead;

	// the distance between tail sections
	private float followDistance;

	// the previous position of the followed object
	private Vector3 followedPrevPosition;

	/// <summary>
	/// Gets or sets the followed object.
	/// </summary>
	/// <value>The followed gameobject.</value>
	public GameObject FollowedObject
	{
		get
		{
			return this.followedObject;
		}
		set
		{
			this.followedObject = value;
		}
	}

	/// <summary>
	/// Gets or sets the snake head script.
	/// </summary>
	/// <value>The snake head script.</value>
	public SnakeHead SnakeHeadScript
	{
		get
		{
			return this.snakeHead;
		}
		set
		{
			this.snakeHead = value;
		}
	}

	/// <summary>
	/// Gets or sets the sequential tail number.
	/// </summary>
	/// <value>The tail number.</value>
	public int TailNumber
	{
		get
		{
			return this.tailNumber;
		}
		set
		{
			this.tailNumber = value;
		}
	}

	/// <summary>
	/// Gets or sets the distance between the tail pieces.
	/// </summary>
	/// <value>The follow distance.</value>
	public float FollowDistance
	{
		get
		{
			return this.followDistance;
		}
		set
		{
			this.followDistance = value;
		}
	}

	/// <summary>
	/// Moves the snake tail section towards an interpolated position near the followed object
	/// </summary>
	private void FollowTarget ()
	{
		// check if the snake head itself is moving
		if (snakeHead.Moving)
		{
			// calculate the vector the object moved during the frame
			Vector3 transition = followedObject.transform.position - followedPrevPosition;

			// check the object being followed moved
			if (transition.magnitude > 0)
			{
				// calculate the vector difference between the tail and the object we're following
				Vector3 posDifference = followedObject.transform.position - transform.position;

				// calculate the speed needed for this frame
				// if the tail is closer than it's supposed to the speed is reduced
				// if it's further then the speed is increased
				float frameSpeed = (posDifference.magnitude / followDistance) * snakeHead.Speed * Time.deltaTime;

				// do interpolation magic on the position difference between the two objects
				posDifference -= (transition / transition.magnitude) * (followDistance / 2);

				// calculate the angle between the tail and the new interpolated position
				float radAngle = Mathf.Atan2 (posDifference.y, posDifference.x);

				// rotate the tail, facing towards the new interpolated position
				transform.eulerAngles = new Vector3 (0, 0, radAngle * Mathf.Rad2Deg);

				// move the tail towards the interpolated position with this frame's speed
				transform.position += new Vector3 (Mathf.Cos (radAngle) * frameSpeed, Mathf.Sin (radAngle) * frameSpeed, 0);

				// set the followed object's previous position to this frame's position
				followedPrevPosition = followedObject.transform.position;
			}
		}
	}

	// Use this for initialization
	void Start () {
		// initialize the followed object's previous position
		followedPrevPosition = followedObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// follow the followed object if necessary
		FollowTarget ();
	}
}
