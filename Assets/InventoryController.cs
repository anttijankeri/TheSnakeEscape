using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {
	public static float inventoryPortion = (float) 0.15;
	public static int inventoryButtonAmount = 4;

	private int currentLeftmostItemSlot = 0;

	private Inventory inventory;
	private List<Button> buttonList;

	private void BrowseLeft ()
	{
		if (currentLeftmostItemSlot > 0)
		{
			currentLeftmostItemSlot--;
			UpdateButtons ();
		}
	}

	private void BrowseRight ()
	{
		if (inventory.ItemTotal > currentLeftmostItemSlot + inventoryButtonAmount)
		{
			currentLeftmostItemSlot++;
			UpdateButtons ();
		}
	}

	private void UseItem (int button)
	{
	}

	private void UpdateButtons ()
	{
		for (int i = 2; i < 2 + inventoryButtonAmount; i++)
		{
			if (inventory.ItemTotal > currentLeftmostItemSlot + i - 2)
			{
				buttonList [i].GetComponent<Image> ().sprite = inventory.GetSprite (currentLeftmostItemSlot + i - 2);
			}
			else
			{
			}
		}
	}

	// Use this for initialization
	void Start () {
		inventory = new Inventory ();

		buttonList = new List<Button> ();

		for (int i = 1; i <= 2 + inventoryButtonAmount; i++)
		{
			buttonList.Add(gameObject.transform.GetChild (i).GetComponent<Button> ());
		}

		buttonList[0].onClick.AddListener(() => BrowseLeft ());
		buttonList[1].onClick.AddListener(() => BrowseRight ());

		for (int i = 2; i < 2 + inventoryButtonAmount; i++)
		{
			int temp = i - 2;
			buttonList[i].onClick.AddListener(() => UseItem(temp));
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
