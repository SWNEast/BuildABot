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

    // Build the list of items - Odd for unlocked, even for locked
    void BuildDatabase() {
        items = new List<Item> {
            new Item(0, "Body", "Items here can be attached to my body", "Body", 0), 
            new Item(1, "????", "??????????", "Locked Legs", 1), 
            new Item(2, "Legs", "Climbing the steps is a breeze with these walkers", "Unlocked Legs", 1), 
            new Item(3, "????", "??????????", "Locked Arms", 2), 
            new Item(4, "Arms", "Finally! Something to pull me up ladders", "Unlocked Arms", 2), 
            
            new Item(5, "????", "??????????", "Locked Glider", 0),
            new Item(6, "Glider", "I bet I can make my jumps last longer using this...", "Unlocked Glider", 0),
            new Item(7, "????", "??????????", "Locked Jet Pack", 0),
            new Item(8, "Jet Pack", "Whoa! I bet I can stay in the air for ages now!", "Unlocked Jet Pack", 0),
            new Item(9, "????", "??????????", "Locked Shield", 0),
            new Item(10, "Shield", "This should help when enemies are hitting me", "Unlocked Shield", 0),
            new Item(11, "????", "??????????", "Locked Gravity", 0),
            new Item(12, "Gravity Changer", "This could let me change the gravity in certain places...", "Unlocked Gravity", 0),

            new Item(13, "????", "??????????", "Locked Spring", 1), 
            new Item(14, "Springs", "I'm sure these could help me spring into the air...", "Unlocked Spring", 1), 
            new Item(15, "????", "??????????", "Locked Wheel", 1), 
            new Item(16, "Wheels", "I'm gonna be able to go so fast now!", "Unlocked Wheel", 1), 
            new Item(17, "????", "??????????", "Locked Flippers", 1),
            new Item(18, "Flippers", "These should help me with swimming", "Unlocked Flippers", 1),
            new Item(19, "????", "??????????", "Locked Boots", 1),
            new Item(20, "Boots", "These are so soft! Wonder if they'd save me from big falls...?", "Unlocked Boots", 1),

            new Item(21, "????", "??????????", "Locked Magnet", 2),
            new Item(22, "Magnets", "I wonder if I could attach to the red, magnetic ceiling with this?", "Unlocked Magnet", 2),
            new Item(23, "????", "??????????", "Locked-Blaster", 2), 
            new Item(24, "Blaster", "I bet I could shoot some enemies with this blaster...", "Unlocked Blaster", 2),
            new Item(25, "????", "??????????", "Locked Umbrella", 2),
            new Item(26, "Umbrella", "I wonder if I could use this for a boost from fans...?", "Unlocked Umbrella", 2),
            new Item(27, "????", "??????????", "Locked Torch", 2),
            new Item(28, "Torch", "This'll help me see in dark places...", "Unlocked Torch", 2)            
        };
    }

    public Item getItem(int id) {
        return items.Find(item => item.id == id);
    }
}
