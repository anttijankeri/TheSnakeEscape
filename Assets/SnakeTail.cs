using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeTail : MonoBehaviour {
	private GameObject followedObject;
	private SnakeHead snakeHead;
	private float followDistance;
	private Vector3 followedPrevPosition;

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

	private void FollowTarget ()
	{
		if (snakeHead.Moving)
		{
			Vector3 posDifference = followedObject.transform.position - transform.position;

			Vector3 transition = followedObject.transform.position - followedPrevPosition;

			float frameSpeed = (posDifference.magnitude / followDistance) * snakeHead.Speed * Time.deltaTime;

			posDifference -= (transition / transition.magnitude) * (followDistance / 2);

			float radAngle = Mathf.Atan2 (posDifference.y, posDifference.x);

			transform.eulerAngles = new Vector3 (0, 0, radAngle * Mathf.Rad2Deg);

			transform.position += new Vector3 (Mathf.Cos (radAngle) * frameSpeed, Mathf.Sin (radAngle) * frameSpeed, 0);

			followedPrevPosition = followedObject.transform.position;
		}
	}

	// Use this for initialization
	void Start () {
		followedPrevPosition = followedObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		FollowTarget ();
	}
}
