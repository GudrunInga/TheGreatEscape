using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemBought
{
	public int itemID;
	public bool bought;
}

public class ShopScrollList : MonoBehaviour {
	public static List<ItemBought> itemList = new List<ItemBought>();
	private static bool _firstRun = true;
	public Transform contentPanel;
	//public static Transform safeContentPanel;

	//public ShopScrollList otherShop;
	public SimpleObjectPool toggleObjectPool;
    private Store storeScript;

	/*public List<ItemBought> GetItemList()
	{
		return itemList;
	}*/
	public void AddItem(ItemBought item)
	{
		itemList.Add (item);
	}
	public bool GetItemByID(int i)
	{
		if (i < itemList.Count) {
			return itemList [i].bought;
		}
		return false;
	}
	// Use this for initialization
	void Start () {
	//	safeContentPanel = contentPanel;
		RefreshDisplay ();
		storeScript = UIController.instance.GetStoreScript ();
	}

	public void RefreshDisplay()
	{
		RemoveButtons ();
		AddButtons ();
	}
    
    public void ActivateDeactivate(bool isActive, ItemManager.Item item)
    {
		//Debug.Log ("Item: " + item.itemName + " script " + storeScript);
		storeScript.SetOwned (ItemManager.instance.GetId (item), isActive);
        //Debug.Log("Toggle me " + item.itemName + " " + isActive);
    }
	private void RemoveButtons()
	{
		while (contentPanel.childCount > 0) 
		{
			GameObject toRemove = transform.GetChild(0).gameObject;
			toggleObjectPool.ReturnObject(toRemove);
		}
	}
	private void AddButtons()
	{
        if (itemList != null && itemList.Count > 0)
        {
			for (int i = 0; i < itemList.Count; i++) {
				//Debug.Log("Size of itemlist" + itemList.Count + " Size of ItemManagerList " + ItemManager.instance.items.Count);
				ItemBought item = itemList [i];//itemList.Count - 1];
				ItemManager.Item itemInfo = ItemManager.instance.items [item.itemID];//itemList.Count - 1];
            	GameObject newToggle = toggleObjectPool.GetObject();

				newToggle.transform.SetParent(contentPanel);
            	SampleButton sampleButton = newToggle.GetComponent<SampleButton>();
				sampleButton.Setup(itemInfo, this);
	            sampleButton.transform.localScale = new Vector3(1, 1, 1);
	            sampleButton.transform.localPosition = new Vector3(sampleButton.transform.localPosition.x, sampleButton.transform.localPosition.y, 0);
			}
		}
	}
}
