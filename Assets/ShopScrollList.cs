using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
	public string itemName;
	public Sprite icon;
	//public float prize = 1f;
}

public class ShopScrollList : MonoBehaviour {
	public List<Item> itemList;
	public Transform contentPanel;
	//public ShopScrollList otherShop;
	public SimpleObjectPool toggleObjectPool;

	// Use this for initialization
	void Start () {
		RefreshDisplay ();
	}

	public void RefreshDisplay()
	{
		AddButtons ();
	}

	private void AddButtons()
	{
		//Check the items we have in our item list
		for (int i = 0; i < itemList.Count; i++) {
			Item item = itemList [i];
			GameObject newToggle = toggleObjectPool.GetObject ();
			newToggle.transform.SetParent (contentPanel);
			SampleButton sampleButton = newToggle.GetComponent<SampleButton> ();
			sampleButton.Setup (item, this);
			sampleButton.transform.localScale = new Vector3(1,1,1);
			sampleButton.transform.localPosition = new Vector3 (sampleButton.transform.localPosition.x, sampleButton.transform.localPosition.y, 0); 
		}
	}
}
