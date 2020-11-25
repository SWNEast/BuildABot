using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Equipped : MonoBehaviour {
    private Item[] equippedItems = {null, null, null};
    public Transform bodyPanel;
    public Transform legsPanel;
    public Transform armsPanel;
    public PlayerMovement player;

    public void EquipItem(Item item, int bodyPart) {
        Image image = null;
        if (bodyPart == 18) { image = bodyPanel.gameObject.GetComponent<Image>(); }
        else if (bodyPart == 19) { image = legsPanel.gameObject.GetComponent<Image>(); }
        else if (bodyPart == 20) { image = armsPanel.gameObject.GetComponent<Image>(); }

        equippedItems[bodyPart-18] = item;
        
        if (item != null) {
            string imgPath = AssetDatabase.GetAssetPath(item.icon);
            imgPath = imgPath.Substring(0, imgPath.Length-4);
            imgPath = imgPath + "hud";
            imgPath = imgPath.Remove(0,17);
            Debug.Log(imgPath);
            Sprite spr = Resources.Load<Sprite>(imgPath);
            image.color = Color.white;
            image.sprite = spr;
        } else {
            image.sprite = Resources.Load<Sprite>("Temp-Images/panel_blue");
            //image.color = Color.clear;
        }
    }

    public bool isEquipped(int id) {
        foreach (Item i in equippedItems) {
            if (i != null)
                if (i.id == id) { return true; }
        }
        return false;
    }

    public bool inHub() {
        return player.inHub;
    }
}
