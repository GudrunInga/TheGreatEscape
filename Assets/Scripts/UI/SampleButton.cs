using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SampleButton : MonoBehaviour {

	public Toggle toggleButton;
	public Image checkMarkImage;
	public Text activateLabel;
	public Image iconImage;

	private ItemManager.Item item;
	private ShopScrollList scrollList;
	// Use this for initialization
	void Start () 
	{
		toggleButton.onValueChanged.AddListener ((value) => {
			if (UIController.instance.IsToggleInteractive ()) {
				ToggleChanged (value);
			}
		});
	}

	public void Setup(ItemManager.Item currentItem, ShopScrollList currentScrollList)
	{
		item = currentItem;
		iconImage.sprite = item.icon;
		scrollList = currentScrollList;
	}

    public void ToggleChanged(bool newValue)
    {
        scrollList.ActivateDeactivate(newValue, item);
    }

}
