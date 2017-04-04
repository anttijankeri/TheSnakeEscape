using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeHead : MonoBehaviour {
	private float speed = (float) 1;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		pz.z = 0;
		pz -= transform.position;
		float len = pz.magnitude;
		pz.Normalize ();
		transform.position += pz * Mathf.Min(speed * Time.deltaTime, len);
		if (len > 0)
		{
			float angle = Mathf.Atan2 (pz.y, pz.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler (0, 0, angle);
		}
	}
}
