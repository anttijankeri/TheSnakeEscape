using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPC : MonoBehaviour {
	private float waitOnHit = 1;
	private float timeToWait = 0;
	private float speed = (float) 0.7;
	private SnakeHead snakeHead;

	// Use this for initialization
	void Start () {
		snakeHead = GameObject.Find ("SnakeHead").GetComponent<SnakeHead> ();
	}

	void OnTriggerEnter (Collider other)
	{
		if (GameObject.Find(other.gameObject.name))
		{
			if (other.gameObject.name.Contains("SnakeTail"))
			{
				timeToWait = waitOnHit;
				snakeHead.AddDamage (other.gameObject);
			}
		}
	}

	// Update is called once per frame
	void Update () {
		if (timeToWait <= 0 && snakeHead.Health > 0)
		{
			Vector3 snakeMidPos = (snakeHead.MiddleSectionPos);
			Vector3 posDifference = snakeMidPos - transform.position;

			transform.position += posDifference.normalized * speed * Time.deltaTime;
		}
		else
		{
			timeToWait -= Time.deltaTime;
		}
	}
}