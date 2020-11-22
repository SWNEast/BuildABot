using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public Transform graphicsPanel;
    public Transform audioPanel;
    public Slider musicSlider;
    public Slider seSlider;
    void Awake() {
        graphicsPanel.gameObject.SetActive(false);
        audioPanel.gameObject.SetActive(false);
        if (SceneManager.GetActiveScene().name == "Main") {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
            seSlider.value = PlayerPrefs.GetFloat("SEVolume");
        }
    }

}
