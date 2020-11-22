using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private Stack<int> loadedScenes;
    public AudioSource music;
    public AudioSource soundEffects;

    void Start() {
        loadedScenes = new Stack<int>();
        AudioListener[] listeners = FindObjectsOfType(typeof(AudioListener)) as AudioListener[];
        int listenerCount = 0;
        foreach (AudioListener audio in listeners) {
            if (audio.enabled == true) { listenerCount = listenerCount + 1; }
        }
        if (listenerCount > 1) {
            GetComponent<AudioListener>().enabled = false;
        } else {
            GetComponent<AudioListener>().enabled = true;
        }
    }

    public void changeScene(string sceneName) {
        loadedScenes.Push(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(sceneName);
    }

    void Update() {
        if (SceneManager.GetActiveScene().name == "Start Menu")
            if (Input.GetKeyDown(KeyCode.Escape)) { backScene(); }
    }

    public void backScene() {
        if (loadedScenes.Count > 0) {
            int toLoad = loadedScenes.Pop();
            loadedScenes.Push(SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.SetInt("AudioListeners", 1);
            SceneManager.LoadScene(toLoad);
        } else {
            PlayerPrefs.SetInt("AudioListeners", 1);
            SceneManager.LoadScene("Start Menu");
        }
    }

    public void changeSceneDelay(string sceneName) {
        loadedScenes.Push(SceneManager.GetActiveScene().buildIndex);
        if (sceneName.Equals("Main with Inventory")) { Invoke("newGame", 1); }
        
    }

    private void newGame() {
        PlayerPrefs.SetFloat("MusicVolume", music.volume);
        PlayerPrefs.SetFloat("SEVolume", soundEffects.volume);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Main with Inventory");
    }

    public void FullScreen(bool full) {
        Screen.fullScreen = full;
    }
}
