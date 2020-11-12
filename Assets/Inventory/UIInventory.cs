using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventory : MonoBehaviour {
    public List<UIItem> uiItems = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    private int noSlots = 20;

    private void Awake() {
        for (int i = 0; i < noSlots; i++) {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(slotPanel);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
        }
    }

    public void UpdateSlot(int slot, Item item) {
        uiItems[slot].UpdateItem(item);
    }

    public void AddItem(Item item) {
        UpdateSlot(uiItems.FindIndex(i => i.item == null), item);
    }

    public void RemoveItem(Item item) {
        UpdateSlot(uiItems.FindIndex(i => i.item == item), null);
    }
}
