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

    public Transform bodyHighlight;
    public Transform legsHighlight;
    public Transform armsHighlight;

    public GameObject player1;
    private PlayerMovement pm;
    private bool holding = false;

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

    public bool inHub()
    {
        return player.inHub;

    }
    private void Start()
    {
        pm = player1.GetComponent<PlayerMovement>();
        bodyHighlight.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        legsHighlight.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        armsHighlight.GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }


    private void Update()
    {
        if (pm.GetSprinting())
        {
            legsHighlight.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else if (pm.GetJumping())
        {
            legsHighlight.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            StartCoroutine(WaitForKeyRelease(KeyCode.X, legsHighlight.GetComponent<Image>()));
        } 
        else if (!holding)
        {
            legsHighlight.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }

        if (pm.GetMagnetOn())
        {
            armsHighlight.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            armsHighlight.GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
    }

    IEnumerator WaitForKeyRelease(KeyCode key, Image highlight)
    {
        holding = true;
        Rigidbody2D rb = player1.GetComponent<Rigidbody2D>();
        Debug.Log("waiting");
        yield return new WaitForSeconds(0.1f);
        while (!Input.GetKeyUp(key) && rb.velocity.y >= 0 && !pm.GetMagnetOn()){
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("released");
        holding = false;
        highlight.color = new Color(1, 1, 1, 0);


    }
}
