using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour {
    // The list of items in the game, ready to be stored in the inventory
    public List<Item> items;
    
    // When the game starts, create the list of items
    private void Awake() {
        BuildDatabase();
    }

    // Build the list of items
    void BuildDatabase() {
        items = new List<Item> {
            new Item(0, "????", "??????????", "Locked Legs"), 
            new Item(1, "Legs", "Climbing the steps is a breeze with these walkers", "Unlocked Legs"), 
            new Item(2, "????", "??????????", "Locked Spring"), 
            new Item(3, "Springs", "Wow! I bet if I put these in my legs I could jump into the air...", "Unlocked Spring"), 
            new Item(4, "????", "??????????", "Locked Arms"), 
            new Item(5, "Arms", "Finally! Something to pull me up ladders", "Unlocked Arms"), 
            new Item(6, "????", "??????????", "Locked-Blaster"), 
            new Item(7, "Blaster", "I bet I could shoot some enemies with this blaster...", "Unlocked Blaster")            
        };
    }

    public Item getItem(int id) {
        return items.Find(item => item.id == id);
    }
}
