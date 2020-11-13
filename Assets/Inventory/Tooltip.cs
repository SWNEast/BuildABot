using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private Text tooltip;

    // Start is called before the first frame update
    void Start() {
        tooltip = GetComponentInChildren<Text>();
        tooltip.gameObject.SetActive(false);
    }

    public void GenerateTooltip(Item item) {
        string tooltip = item.title + "\n" + item.description;
        this.tooltip.text = tooltip;
        gameObject.SetActive(true);
        this.tooltip.gameObject.SetActive(true);
    }
}
