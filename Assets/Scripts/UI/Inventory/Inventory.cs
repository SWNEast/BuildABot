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
    public Transform optionsMenu;
    public Transform menu;
    public AudioSource music;
    public AudioSource soundEffects;
    public AudioSource openMenu;

    private void Start() {
        menu.gameObject.SetActive(false);
        inventoryUI.gameObject.SetActive(false);
        panel.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(false);
        Debug.Log(PlayerPrefs.GetFloat("MusicVolume"));
        music.volume = PlayerPrefs.GetFloat("MusicVolume");
        soundEffects.volume = PlayerPrefs.GetFloat("SEVolume");
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
        }if (id > 4 && !characterItems.Contains(found)) { giveItem(id); }
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
            openMenu.Play();
            inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
            panel.gameObject.SetActive(!panel.gameObject.activeSelf);
            invOpenBtn.gameObject.SetActive(!invOpenBtn.gameObject.activeSelf);
            invCloseBtn.gameObject.SetActive(!invCloseBtn.gameObject.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            openMenu.Play();
            if (inventoryUI.gameObject.activeSelf == false && optionsMenu.gameObject.activeSelf == false) {
                menu.gameObject.SetActive(!menu.gameObject.activeSelf);
                panel.gameObject.SetActive(!panel.gameObject.activeSelf);
            } else if (inventoryUI.gameObject.activeSelf == true) {
                inventoryUI.gameObject.SetActive(!inventoryUI.gameObject.activeSelf);
                panel.gameObject.SetActive(!panel.gameObject.activeSelf);
                invOpenBtn.gameObject.SetActive(!invOpenBtn.gameObject.activeSelf);
                invCloseBtn.gameObject.SetActive(!invCloseBtn.gameObject.activeSelf);
            } else if (optionsMenu.gameObject.activeSelf == true) {
                optionsMenu.gameObject.SetActive(false);
                panel.gameObject.SetActive(false);
            }
        }
    }
}
