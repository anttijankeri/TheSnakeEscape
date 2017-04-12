using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour {
	private int health = 20;
	private float tailDistance = (float) 0.12;
	private float speed;
	private bool moving;

	private List<SnakeTail> tailList;
	private GameObject snakeTailPrefab;
	private PlayerController playerController;

	private Vector3 prevPosition;

	private GameObject AddTailPiece (GameObject following, Vector3 position)
	{
		GameObject newTail = Instantiate (snakeTailPrefab, position, transform.rotation);
		SnakeTail newScript = newTail.GetComponent<SnakeTail> ();
		newScript.FollowedObject = following;
		newScript.SnakeHeadScript = this;
		newScript.FollowDistance = tailDistance;
		tailList.Add (newScript);
		return newTail;
	}

	public bool Moving
	{
		get
		{
			return this.moving;
		}
		set
		{
			this.moving = value;
		}
	}

	public float Speed
	{
		get
		{
			return this.speed;
		}
		set
		{
			this.speed = value;
		}
	}

	// Use this for initialization
	void Start () {
		playerController = this.gameObject.GetComponent<PlayerController> ();
		snakeTailPrefab = Resources.Load ("SnakeTail") as GameObject;
		prevPosition = new Vector3 (transform.position.x, transform.position.y, transform.position.z);

		GameObject following = this.gameObject;
		tailList = new List<SnakeTail> ();
		Vector3 position = transform.position;

		for (int i = 0; i < health; i++)
		{
			position.x -= tailDistance;
			following = AddTailPiece (following, position);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Speed = playerController.GetRealSpeed ();
		Moving = (prevPosition != transform.position) ? true : false;
		prevPosition = transform.position;
	}
}
