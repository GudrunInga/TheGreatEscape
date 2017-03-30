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
	private static bool firstRun = true;
	// Use this for initialization
	void Start () {
	//	safeContentPanel = contentPanel;
		//Debug.Log("I am in Start");
		RefreshDisplay ();
		storeScript = UIController.instance.GetStoreScript ();


	}

	public void RefreshDisplay()
	{
		
		//Debug.Log("REFRESH THE DISPLAY!" + firstRun);
		RemoveButtons ();
		AddButtons ();
		firstRun = false;
	}
    
    public void ActivateDeactivate(bool isActive, ItemManager.Item item)
    {
		//Debug.Log ("ACTIVATE DEACTIVATE " + item.itemName + " ACTIVE? " + isActive);
		UIController.instance.GetStoreScript ().SetOwned (ItemManager.instance.GetId (item), isActive);
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
			foreach (var item in itemList) {
				ItemManager.Item itemInfo = ItemManager.instance.items [item.itemID];//itemList.Count - 1];
				GameObject newToggle = toggleObjectPool.GetObject();
				newToggle.transform.SetParent(contentPanel);
				SampleButton sampleButton = newToggle.GetComponent<SampleButton>();
				sampleButton.Setup(itemInfo, this);

				if (item.itemID < 6) {
					if (!UIController.instance.GetStoreScript ().GetOwnedFormByID (item.itemID + 1)) {
						UIController.instance.SetToggleInteractive (false);
						sampleButton.GetComponent<Toggle> ().interactable = false;
						sampleButton.GetComponent<Toggle> ().isOn = false;
						sampleButton.GetComponent<Toggle> ().interactable = true;
						UIController.instance.SetToggleInteractive (true);
					} else {
						UIController.instance.SetToggleInteractive (true);
						sampleButton.GetComponent<Toggle> ().interactable = true;
						sampleButton.GetComponent<Toggle> ().isOn = true;
						sampleButton.GetComponent<Toggle> ().interactable = true;
						UIController.instance.SetToggleInteractive (true);
					}
				} /*else {
					if (!UIController.instance.GetStoreScript ().GetOwnedHatByID (item.itemID)) {
						UIController.instance.SetToggleInteractive (false);
						sampleButton.GetComponent<Toggle> ().interactable = false;
						sampleButton.GetComponent<Toggle> ().isOn = false;
						sampleButton.GetComponent<Toggle> ().interactable = true;
						UIController.instance.SetToggleInteractive (true);
					} else {
						UIController.instance.SetToggleInteractive (true);
						sampleButton.GetComponent<Toggle> ().interactable = true;
						sampleButton.GetComponent<Toggle> ().isOn = true;
						sampleButton.GetComponent<Toggle> ().interactable = true;
						UIController.instance.SetToggleInteractive (true);
					}
					AddFancyButtons (sampleButton, item.itemID);
				}*/
				//if (item.itemID >= 6) {
					
					//AddFancyButtons (sampleButton, itemList [i].itemID);
				//} else {
					//if (sampleButton.tag == "Untagged") {
					//sampleButton.tag = "balloonForm";
				//}
				//}
				sampleButton.transform.localScale = new Vector3(1, 1, 1);
				sampleButton.transform.localPosition = new Vector3(sampleButton.transform.localPosition.x, sampleButton.transform.localPosition.y, 0);
			}
			/*for (int i = 0; i < itemList.Count; i++) {
				ItemBought item = itemList [i];
				//Debug.Log ("ITEM ID: " + item.itemID);
				ItemManager.Item itemInfo = ItemManager.instance.items [item.itemID];//itemList.Count - 1];
            	GameObject newToggle = toggleObjectPool.GetObject();
				newToggle.transform.SetParent(contentPanel);
            	SampleButton sampleButton = newToggle.GetComponent<SampleButton>();
				//Debug.Log ("ID OF BUTTON: " + sampleButton.tag);
				sampleButton.Setup(itemInfo, this);

				if (itemList [i].itemID >= 6) {
					//AddFancyButtons (sampleButton, itemList [i].itemID);
				} else {
				//if (sampleButton.tag == "Untagged") {
					//sampleButton.tag = "balloonForm";
				}
				//}
	            sampleButton.transform.localScale = new Vector3(1, 1, 1);
	            sampleButton.transform.localPosition = new Vector3(sampleButton.transform.localPosition.x, sampleButton.transform.localPosition.y, 0);
			}*/
		}
	}
	private void AddFancyButtons(SampleButton button, int id)
	{
		//Debug.Log ("WE ARE IN ITEM LIST! " + button.tag + " ID " + id);
		//if (button.tag != "balloonForm") {
		GameObject topHat = UIController.instance.player.transform.GetChild (0).gameObject;;
		GameObject bow = UIController.instance.player.transform.GetChild (1).gameObject;
		GameObject cap = UIController.instance.player.transform.GetChild (3).gameObject;
		if (id == 6) {
			button.tag = "ToggleHat";
		} else if (id == 7) {
			button.tag = "ToggleBow";
		} else if (id == 8) {
			button.tag = "ToggleCap";
		}
		//}

		/*if (UIController.instance.GetStoreScript ().GetOwnedHatByID (id)) {
			UIController.instance.SetActiveFancyStuff (id - 6, true);
		}*/
		//This seems to be okay and working as it is supposed to
		if (UIController.instance.GetStoreScript ().GetStoreOwnedItem (id)) {
			if (id == 6) {
				topHat.SetActive (true);
				if (bow.activeSelf) {
					bow.SetActive (false);
				}
				if (cap.activeSelf) {
					cap.SetActive (false);
				}
			}
			if (id == 7) {
				bow.SetActive (true);
				if (topHat.activeSelf) {
					topHat.SetActive (false);
				}
				if (cap.activeSelf) {
					cap.SetActive (false);
				}
			}
			if (id == 8) {
				cap.SetActive (true);
				if (topHat.activeSelf) {
					topHat.SetActive (false);
				}
				if (bow.activeSelf) {
					bow.SetActive (false);
				}
			}
		} else {
			if (id == 6) {
				topHat.SetActive (false);
			} else if (id == 7) {
				bow.SetActive (false);
			} else if (id == 8) {
				cap.SetActive (false);
			}
		}
		/*button.GetComponent<Toggle> ().interactable = false;

		if (button.tag == "ToggleHat") {
			Debug.Log ("Tag is a togglehat");
			var obj = GameObject.FindGameObjectWithTag ("Tophat");
			if(obj != null){
				//Debug.Log ("I FOUND HAT " + obj.activeSelf);
				if (obj.activeSelf) {
					button.GetComponent<Toggle> ().isOn = true;

				} else {
					button.GetComponent<Toggle> ().isOn = false;
				}

			}else {
				button.GetComponent<Toggle> ().isOn = false;
			}
		}
		if (button.tag == "ToggleBow") {
			Debug.Log ("Tag is a togglebow");
			var obj = GameObject.FindGameObjectWithTag ("Bow");
			if(obj != null){
				//Debug.Log ("I FOUND BOW " + obj.activeSelf);
				if (obj.activeSelf) {
					button.GetComponent<Toggle> ().isOn = true;
				} else {
					button.GetComponent<Toggle> ().isOn = false;
				}
			}
			else {
				button.GetComponent<Toggle> ().isOn = false;
			}
		}
		if (button.tag == "ToggleCap") {
			Debug.Log ("Tag is a togglecap");
			var obj = GameObject.FindGameObjectWithTag ("BaseballCap");
			if(obj != null){
				//Debug.Log ("I FOUND BASEBALLCAP " + obj.activeSelf);
				if (obj.activeSelf) {
					button.GetComponent<Toggle> ().isOn = true;
				} else {
					button.GetComponent<Toggle> ().isOn = false;
				}
			}
			else {
				button.GetComponent<Toggle> ().isOn = false;
			}
		}
		//Debug.Log ("SAMPLE BUTTON IS ON?: " + sampleButton.GetComponent<Toggle> ().isOn); 
		button.GetComponent<Toggle> ().interactable = true;
		//UIController.instance.SetActiveFancyStuff (itemList [i].itemID - 6, false);
		*/
	}
	public int GetSizeOfList()
	{
		return itemList.Count;
	}
}
