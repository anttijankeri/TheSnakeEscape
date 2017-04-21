using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {
	public static float inventoryPortion = 0.15f;
	public static int inventoryButtonAmount = 4;

	private int currentLeftmostItemSlot = 0;

	private Inventory inventory;
	private List<Button> buttonList;

	public static GameObject inventoryCanvas;

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

	private void ClickItemButton (int button)
	{
		if (inventory.ItemTotal > currentLeftmostItemSlot + button)
		{
			inventory.UseItem (currentLeftmostItemSlot + button);

			if (currentLeftmostItemSlot > inventory.ItemTotal - inventoryButtonAmount)
			{
				currentLeftmostItemSlot = Mathf.Max (0, currentLeftmostItemSlot - 1);
			}

			UpdateButtons ();
		}
	}

	private void UpdateButtons ()
	{
		for (int i = 2; i < 2 + inventoryButtonAmount; i++)
		{
			if (inventory.ItemTotal > currentLeftmostItemSlot + i - 2)
			{
				buttonList [i].GetComponent<Image> ().enabled = true;
				buttonList [i].GetComponent<Image> ().sprite = inventory.GetSprite (currentLeftmostItemSlot + i - 2);
			}
			else
			{
				buttonList [i].GetComponent<Image> ().enabled = false;
			}
		}
	}

	void Awake ()
	{
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
		inventoryCanvas = gameObject;

		inventory = new Inventory ();

		buttonList = new List<Button> ();

		for (int i = 1; i <= 2 + inventoryButtonAmount; i++)
		{
			buttonList.Add (gameObject.transform.GetChild (i).GetComponent<Button> ());
		}

		buttonList [0].onClick.AddListener (() => BrowseLeft ());
		buttonList [1].onClick.AddListener (() => BrowseRight ());

		for (int i = 2; i < 2 + inventoryButtonAmount; i++)
		{
			int temp = i - 2;
			buttonList [i].onClick.AddListener (() => ClickItemButton (temp));
		}

		UpdateButtons ();
	}
}