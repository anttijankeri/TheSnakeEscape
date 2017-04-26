using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/// <summary>
/// A single waypoint with a position relative to the gameobject
/// </summary>
public class Waypoint {
	[SerializeField]
	public Vector3 position;
}