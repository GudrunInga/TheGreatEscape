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
    private Store storeScript;

	// Use this for initialization
	void Start () {
		RefreshDisplay ();
        storeScript = UIController.instance.GetStoreScript();

	}

	public void RefreshDisplay()
	{

		AddButtons ();
	}
    private void InitializeItemList()
    {
        List<bool> tempList = storeScript.GetOwnedForms();
        for(int i = 1; i < 7; i++)
        {

        }
    }
    public void ActivateDeactivate(bool isActive, Item item)
    {
        //Very ugly code:
        if(item.itemName == "Cat")
        {
            storeScript.SetOwned(1, isActive);
        }
        if (item.itemName == "Dog")
        {
            storeScript.SetOwned(2, isActive);
        }
        if (item.itemName == "Scissors")
        {
            storeScript.SetOwned(3, isActive);
        }
        if (item.itemName == "Gas")
        {
            storeScript.SetOwned(4, isActive);
        }
        if (item.itemName == "Sword")
        {
            storeScript.SetOwned(5, isActive);
        }
        if (item.itemName == "Lead")
        {
            storeScript.SetOwned(6, isActive);
        }


        Debug.Log("Toggle me " + item.itemName + " " + isActive);
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
