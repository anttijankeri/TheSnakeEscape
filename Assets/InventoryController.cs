using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {
	public static float inventoryPortion = (float) 0.15;
	public static int inventoryButtonAmount = 4;

	private Inventory inventory;
	private List<Button> buttonList;

	private void BrowseLeft ()
	{
	}

	// Use this for initialization
	void Start () {
		inventory = new Inventory ();

		buttonList = new List<Button> ();
		for (int i = 1; i <= 6; i++)
		{
			buttonList.Add (gameObject.transform.GetChild (i).gameObject.GetComponent<Button> ());
		}

		buttonList[0].onClick.AddListener(() => );
		buttonList[1].onClick.AddListener(() => );

		for (int i = 3; i <= 6; i++)
		{
			
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
