using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenu : MonoBehaviour {

    // Start is called before the first frame update
    void Start() {
        this.gameObject.SetActive(false);
    }

    public void ToggleOn() {
        this.gameObject.SetActive(!this.gameObject.activeSelf);
    }
}
