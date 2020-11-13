using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvButton : MonoBehaviour
{
    public UIInventory inventory;
    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("Clicked");
        inventory.gameObject.SetActive(true);
    }
}
