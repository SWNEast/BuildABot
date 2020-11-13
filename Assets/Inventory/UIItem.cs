using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler {
    public Item item;
    private Image spriteImage;
    private UIItem selectedItem;
    private Tooltip tooltip;

    // When game started, assign the image to the UIItem, find the selected item
    public void Awake() {
        spriteImage = GetComponent<Image>();
        UpdateItem(null);
        selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
        tooltip = GameObject.Find("Tooltip").GetComponent<Tooltip>();
    }

    // Assign the new item when updating, if given null show nothing
    public void UpdateItem(Item item) {
        this.item = item;
        if (this.item != null) {
            spriteImage.color = Color.white;
            spriteImage.sprite = item.icon;
        } else {
            spriteImage.color = Color.clear;
        }
    }

    // Method runs if the item is clicked
    public void OnPointerClick(PointerEventData eventData) {
            if (this.item != null) {                                // If the player clicks on an actual item:
            if (this.item.id % 2 != 0) {
                if (selectedItem.item != null) {                        // If there was already a selected item, clone it and replace it with the item clicked on
                    Item clone = new Item(selectedItem.item); 
                    selectedItem.UpdateItem(this.item);
                    UpdateItem(clone);                                  // Puts the cloned item in the place of where the old item used to be
                } else {
                    selectedItem.UpdateItem(this.item);             // If there wasn't a selected item, just select this item and replace it with nothing
                    UpdateItem(null);
                }
            }
            } else if (selectedItem.item != null) {                 // If there was no item in the slot, drop the selected item in said place
                UpdateItem(selectedItem.item);
                selectedItem.UpdateItem(null);
            }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (this.item != null)
            tooltip.GenerateTooltip(this.item);
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltip.gameObject.SetActive(false);
    }
}
