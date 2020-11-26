using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour {
    public List<UIItem> uiItems = new List<UIItem>();
    public GameObject slotPrefab;
    public GameObject lockedSlotPrefab;
    public Transform invPanel;
    public Transform storagePanelParent;
    public Transform storagePanelBody;
    public Transform storagePanelLegs;
    public Transform storagePanelArms;
    public Transform charPanel;

    private void Awake() {
        for (int i = 0; i < 3; i++) {
            GameObject instance = Instantiate(slotPrefab);
            instance.GetComponent<Image>().color = Color.gray;
            instance.transform.SetParent(storagePanelParent);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
            uiItems[i].SetSlot(i);
        }
        for (int i = 0; i < 4; i++) {
            GameObject instance = Instantiate(slotPrefab);
            instance.GetComponent<Image>().color = Color.gray;
            instance.transform.SetParent(storagePanelBody);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
            uiItems[i+3].SetSlot(i+3);
        }
        for (int i = 0; i < 4; i++) {
            GameObject instance = Instantiate(slotPrefab);
            instance.GetComponent<Image>().color = Color.gray;
            instance.transform.SetParent(storagePanelLegs);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
            uiItems[i+7].SetSlot(i+7);
        }
        for (int i = 0; i < 4; i++) {
            GameObject instance = Instantiate(slotPrefab);
            instance.GetComponent<Image>().color = Color.gray;
            instance.transform.SetParent(storagePanelArms);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
            uiItems[i+11].SetSlot(i+11);
        }
        for (int i = 0; i < 3; i++) {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(invPanel);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
            uiItems[i+15].SetSlot(i+15);
        }
        
        for (int i = 0; i < 3; i++) {
            GameObject instance = Instantiate(slotPrefab);
            instance.transform.SetParent(charPanel);
            uiItems.Add(instance.GetComponentInChildren<UIItem>());
            uiItems[i+18].SetSlot(i+18);
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

    public void ReplaceItem(Item oldItem, Item newItem) {
        UpdateSlot(uiItems.FindIndex(i => i.item == oldItem), newItem);

    }

    public void openHub() {
        for (int i = 0; i < 3; i ++) {
            Transform storagePanel = storagePanelParent.GetChild(i);
            storagePanel.GetComponent<Image>().color = Color.white;
        }
        for (int i = 0; i < 4; i ++) { 
            Transform storagePanel = storagePanelBody.GetChild(i);
            storagePanel.GetComponent<Image>().color = Color.white;
        }
        for (int i = 0; i < 4; i ++) { 
            Transform storagePanel = storagePanelLegs.GetChild(i);
            storagePanel.GetComponent<Image>().color = Color.white;
        }
        for (int i = 0; i < 4; i ++) { 
            Transform storagePanel = storagePanelArms.GetChild(i);
            storagePanel.GetComponent<Image>().color = Color.white;
        }unEquipAll();
    }

    public void closeHub() {
        for (int i = 0; i < 3; i ++) {
            Transform storagePanel = storagePanelParent.GetChild(i);
            storagePanel.GetComponent<Image>().color = Color.gray;
        }
        for (int i = 0; i < 4; i ++) { 
            Transform storagePanel = storagePanelBody.GetChild(i);
            storagePanel.GetComponent<Image>().color = Color.gray;
        }
        for (int i = 0; i < 4; i ++) { 
            Transform storagePanel = storagePanelLegs.GetChild(i);
            storagePanel.GetComponent<Image>().color = Color.gray;
        }
        for (int i = 0; i < 4; i ++) { 
            Transform storagePanel = storagePanelArms.GetChild(i);
            storagePanel.GetComponent<Image>().color = Color.gray;
        }
    }

    public void unEquipAll() {
        for (int i = 0; i < 3; i ++) {
            Transform panel = invPanel.GetChild(i);
            Debug.Log(panel + " " + panel.GetComponentInChildren<UIItem>());
            UIItem item = panel.GetComponentInChildren<UIItem>();
            item.UpdateItem(null);
        }
        for (int i = 0; i < 3; i ++) {
            Transform panel = charPanel.GetChild(i);
            UIItem item = panel.GetComponentInChildren<UIItem>();
            item.UpdateItem(null);
        }
    }
}
