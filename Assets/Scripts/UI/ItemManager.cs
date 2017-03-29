using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour {

	[System.Serializable]
	public class Item
	{
		public string itemName;
		public Sprite icon;
		public Text text;
		//public int ID;
		public int cost;
		//public bool bought;
	}
	public List<Item> items;

	public static ItemManager instance;
	public void Awake()
	{
		instance = this;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public int GetId(ItemManager.Item item)
	{
		//Debug.Log ("Item Manager get id: " + items.IndexOf(item));
		return items.IndexOf (item);
	}
}
