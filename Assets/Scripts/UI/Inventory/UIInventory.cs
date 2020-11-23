using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour {
    public List<UIItem> uiItems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform invPanel;
    public Transform storagePanelParent;
    public Transform storagePanelBody;
    public Transform storagePanelLegs;
    public Transform storagePanelArms;
    public Transform charPanel;
    //public Equipped equipped;

    private void Awake() {
        for (int i = 0; i < 3; i++) {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(storagePanelParent);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
        }
        for (int i = 0; i < 4; i++) {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(storagePanelBody);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
        }
        for (int i = 0; i < 4; i++) {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(storagePanelLegs);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
        }
        for (int i = 0; i < 4; i++) {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(storagePanelArms);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
        }
        for (int i = 0; i < 3; i++) {

            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(invPanel);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
        }
        
        for (int i = 0; i < 3; i++) {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(charPanel);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
            uiItems[i+18].SetSlot(i);
        }
    }

    public void UpdateSlot(int slot, Item item) {
        uiItems[slot].UpdateItem(item);
        /*if (slot >= 18) {
            Debug.Log("In Character Panel");
            if (item == null) {
                if (slot == 18) { equipped.EquipItem(null, 0); }
                else if (slot == 19) { equipped.EquipItem(null, 1); }
                else if (slot == 20) { equipped.EquipItem(null, 2); }
            } else{ equipped.EquipItem(item, item.category); }
        }*/
    }

    public void AddItem(Item item) {
        UpdateSlot(uiItems.FindIndex(i => i.item == null), item);
    }

    public void RemoveItem(Item item) {
        UpdateSlot(uiItems.FindIndex(i => i.item == item), null);
    }

    public void ReplaceItem(Item oldItem, Item newItem) {
        UpdateSlot(uiItems.FindIndex(i => i.item == oldItem), newItem);

    }
}
