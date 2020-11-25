using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvTutorial : MonoBehaviour {
    
    private bool clicked = false;
    private bool isBlinking = false;
    public AudioSource click;

    void Start() {
        this.enabled = false;
        startBlink();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) { CancelInvoke(); click.Play(); this.enabled = false; }
    }

    private void showHide() {
        //gameObject.SetActive(this.enabled = !this.enabled);
        gameObject.SetActive(false);
    }

    private void startBlink() {
        if (isBlinking) { return; }
        else {
            isBlinking = true;
            InvokeRepeating("showHide", 0f, 1f);
        }
    }
}
