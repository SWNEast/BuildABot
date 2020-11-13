using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public List<Item> characterItems = new List<Item>();
    public ItemDatabase itemList;
    public UIInventory inventoryUI;
    public Transform panel;

    private void Start() {
        inventoryUI.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
        giveItem(0);
        giveItem(2);
        giveItem(4);
        giveItem(6);
    }

    // Takes the locked item out the player's inventory, gives the proper item
    public void foundItem(int id) {
        Item toRemove = checkForItem(id-1);
        Item found = itemList.items[id];
        if (toRemove != null) {
            characterItems.Remove(toRemove);
            Debug.Log("Removed item " + toRemove.title);
            
            if (found != null) { inventoryUI.ReplaceItem(toRemove, found); }
            else { inventoryUI.RemoveItem(toRemove); }
        }
        characterItems.Add(found);
    }

    // Gives the player an item based off the item id
    private void giveItem(int id) {
        Item foundItem = itemList.getItem(id);
        characterItems.Add(foundItem);
        Debug.Log("Added item " + foundItem.title);
        inventoryUI.AddItem(foundItem);
    }

    private Item checkForItem(int id) {
        return characterItems.Find(item => item.id == id);
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.I)) {
            inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
            panel.gameObject.SetActive(!panel.gameObject.activeSelf);
        }
    }
}
