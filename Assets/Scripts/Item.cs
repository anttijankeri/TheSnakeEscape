using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
	private Sprite sprite;
	private string itemName;
	private bool puzzleItem = false;

	public Item (string itemName = "Apple", bool puzzleItem = false)
	{
		this.itemName = itemName;

		sprite = (Sprite) Resources.Load ("Sprites/Items/" + this.itemName, typeof(Sprite));
		this.puzzleItem = puzzleItem;
	}

	public bool PuzzleItem
	{
		get
		{
			return this.puzzleItem;
		}
	}

	public Sprite ItemSprite
	{
		get
		{
			return sprite;
		}
	}
}