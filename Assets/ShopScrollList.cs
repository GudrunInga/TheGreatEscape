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
        if (itemList.Count > 0)
        {
            Debug.Log("Size of itemlist" + itemList.Count);
            Item item = itemList[itemList.Count - 1];
            GameObject newToggle = toggleObjectPool.GetObject();
            newToggle.transform.SetParent(contentPanel);
            SampleButton sampleButton = newToggle.GetComponent<SampleButton>();
            sampleButton.Setup(item, this);
            sampleButton.transform.localScale = new Vector3(1, 1, 1);
            sampleButton.transform.localPosition = new Vector3(sampleButton.transform.localPosition.x, sampleButton.transform.localPosition.y, 0);
        }
	}
}
