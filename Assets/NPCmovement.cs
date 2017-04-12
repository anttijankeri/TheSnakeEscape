using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCmovement : MonoBehaviour {
	private float speed = (float) 0.65;
	private GameObject snakeHead;


	// Use this for initialization
	void Start () {
		snakeHead = GameObject.Find ("Snake");
	}

	// Update is called once per frame
	void Update () {
		Vector3 pz = (snakeHead.transform.position - transform.position);
		pz.z = 0;
		float len = pz.magnitude;

		if (len <= 1) {
			
			pz.Normalize ();
			transform.position += pz * Mathf.Min (speed * Time.deltaTime, len);

				

		}
	}
}
