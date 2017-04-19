using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {
	private List<Item> itemList;

	public Inventory ()
	{
		itemList = new List<Item> ();
	}

	public int ItemTotal
	{
		get
		{
			return 0;
		}
	}

	public Sprite GetSprite (int itemNumber)
	{
		return itemList [itemNumber].ItemSprite;
	}
}