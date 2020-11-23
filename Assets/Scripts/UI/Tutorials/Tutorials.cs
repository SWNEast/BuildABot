using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorials : MonoBehaviour
{
    public PlayerMovement player;
    void Update() {
        if (Input.GetMouseButtonDown(0)) { gameObject.SetActive(false); player.setMovement(true); }
        if (Input.GetKeyDown(KeyCode.Space)) { gameObject.SetActive(false); player.setMovement(true); }
    }
}
