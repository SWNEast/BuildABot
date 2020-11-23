using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Equipped : MonoBehaviour {
    private Item[] equippedItems = {null, null, null};
    public Transform bodyPanel;
    public Transform legsPanel;
    public Transform armsPanel;

    public void EquipItem(Item item, int bodyPart) {
        Image image = null;
        if (bodyPart == 0) { image = bodyPanel.gameObject.GetComponent<Image>(); }
        else if (bodyPart == 1) { image = legsPanel.gameObject.GetComponent<Image>(); }
        else if (bodyPart == 2) { image = armsPanel.gameObject.GetComponent<Image>(); }

        if (item != null) {
            Debug.Log("Equipped " + item.icon);
            equippedItems[bodyPart] = item;
            image.color = Color.white;
            image.sprite = item.icon;
        } else {
            image.color = Color.clear;
        }
    }

    public bool isEquipped(int id) {
        foreach (Item i in equippedItems) {
            if (i != null)
                if (i.id == id) { return true; }
            else
                return false;
        }
        return false;
    }
}
