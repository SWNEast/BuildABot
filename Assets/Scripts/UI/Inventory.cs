using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public List<Item> characterItems = new List<Item>();
    public ItemDatabase itemList;
    public UIInventory inventoryUI;
    public Transform panel;
    public Transform invOpenBtn;
    public Transform invCloseBtn;

    private void Start() {
        inventoryUI.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
        giveItem(0);
        for (int i=1; i<itemList.items.Count; i++) {
            if (i % 2 != 0) { giveItem(i); }
        }
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
            invOpenBtn.gameObject.SetActive(!invOpenBtn.gameObject.activeSelf);
            invCloseBtn.gameObject.SetActive(!invCloseBtn.gameObject.activeSelf);
        }
    }
}
