﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {
    public int id;
    public string title;
    public string description;
    public Sprite icon;
    public int category;

    public Item(int id, string title, string description, string iconName, int category) {
        this.id = id;
        this.title = title;
        this.description = description;
        this.icon = Resources.Load<Sprite>("Temp-Images/" + iconName);
        this.category = category;
    }

    public Item(Item item) {
        this.id = item.id;
        this.title = item.title;
        this.description = item.description;
        this.icon = item.icon;
    }

}
