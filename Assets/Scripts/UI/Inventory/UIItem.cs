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
    private int slot = 999;
    public Equipped equipped;
    public AudioSource click;
    public PlayerMovement player;
    private bool locked = false;
    private bool inHub = false;

    // When game started, assign the image to the UIItem, find the selected item
    public void Awake() {
        spriteImage = GetComponent<Image>();
        UpdateItem(null);
        selectedItem = GameObject.Find("SelectedItem").GetComponent<UIItem>();
        tooltip = GameObject.Find("Tooltip").GetComponent<Tooltip>();
        equipped = GameObject.Find("Equipped Master Panel").GetComponent<Equipped>();
        click = GameObject.Find("Click Sound").GetComponent<AudioSource>();
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Assign the new item when updating, if given null show nothing
    public void UpdateItem(Item item) {
        this.item = item;
        if (this.item != null) {
            if (slot < 15 && !inHub) {
                spriteImage.color = Color.gray;
                spriteImage.sprite = item.icon;
            }else {
                spriteImage.color = Color.white;
                spriteImage.sprite = item.icon;
            }
        } else {
            spriteImage.color = Color.clear;
        }
    }

    // Method runs if the item is clicked
    public void OnPointerClick(PointerEventData eventData) {
        click.Play();
        if (this.item != null) {                                // If the player clicks on an actual item:
            if ((item.id > 4 && slot >= 15) || (inHub && item.id > 4 && (item.id % 2 == 0))) {
                if (selectedItem.item != null) {                        // If there was already a selected item, clone it and replace it with the item clicked on
                    Item clone = new Item(selectedItem.item);                           // Puts the cloned item in the place of where the old item used to be
                    if (this.slot >= 18) { 
                        if ((slot-18) == selectedItem.item.category) {
                            selectedItem.UpdateItem(this.item);
                            clone.category = slot-18;
                            UpdateItem(clone);
                            equipped.EquipItem(clone, slot);
                        }
                    } else {
                        selectedItem.UpdateItem(this.item);
                        UpdateItem(clone);
                        equipped.EquipItem(clone, slot);
                    }
                } else {
                    if (this.slot >= 18) { 
                        equipped.EquipItem(null, slot);
                    }
                    selectedItem.UpdateItem(this.item);             // If there wasn't a selected item, just select this item and replace it with nothing
                    UpdateItem(null);
                }
            }
        } else if (selectedItem.item != null) {                 // If there was no item in the slot, drop the selected item in said place
            if (slot >= 18) {
                if ((slot-18) == selectedItem.item.category) {
                    UpdateItem(selectedItem.item);
                    equipped.EquipItem(selectedItem.item, slot);
                    selectedItem.UpdateItem(null);
                }
            } else if ((slot >= 15 && slot <= 17) || (inHub)) {
                int tempSlot = selectedItem.slot;
                UpdateItem(selectedItem.item);
                //selectedItem.SetSlot(tempSlot);
                selectedItem.UpdateItem(null);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (this.item != null) {
            tooltip.GenerateTooltip(this.item);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData) {
        tooltip.gameObject.SetActive(false);
        
    }

    public void SetSlot(int slot) {
        this.slot = slot;
    }

    void Update() {
        if (slot < 15) {
            if (player.inHub) {
                inHub = true;
                UpdateItem(this.item);
            } else   
                inHub = false;
        }
    }
}
