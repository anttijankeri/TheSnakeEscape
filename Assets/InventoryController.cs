using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour {
	public static float inventoryPortion = (float) 0.15;
	public static int inventoryButtonAmount = 4;
	private Inventory inventory;
	private Canvas inventoryCanvas;

	// Use this for initialization
	void Start () {
		inventoryCanvas = gameObject.GetComponent<Canvas> ();

	}

	// Update is called once per frame
	void Update () {
		
	}
}
