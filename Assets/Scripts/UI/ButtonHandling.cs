using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHandling : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    public AudioSource click;

    public void OnPointerEnter(PointerEventData eventData) {
        Text text = this.GetComponent<Button>().GetComponentInChildren<Text>();
        text.fontStyle = FontStyle.Bold;
        click.Play();
    }

    public void OnPointerExit(PointerEventData eventData) {
        Text text = this.GetComponent<Button>().GetComponentInChildren<Text>();    
        text.fontStyle = FontStyle.Normal;
    }
}
