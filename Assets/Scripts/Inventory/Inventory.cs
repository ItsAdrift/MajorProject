using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int size = 1;
    public int stack = 20;
    public Slot[] slots;

    private void Awake()
    {
        slots = new Slot[size];
        for (int i = 0; i < size; i++)
        {
            slots[i] = new Slot();
        }
    }

    public bool AddItem(Item item)
    {
        foreach (Slot slot in slots)
        {
            if (slot.GetItem() != null && slot.GetItem().type != item.type)
                continue;
            if (slot.GetAmount() >= stack)
                continue;

            if (slot.GetItem() == null)
                slot.SetItem(item);

                                 // 20   // 14
            int availableSpace = stack - slot.GetAmount(); // Available space in the slot
            //  availableSpace = 6

                // 19         // 6
            if (item.amount > availableSpace)
            {
                             // 19          // 19         // 6
                slot.AddItem(item.amount - (item.amount - availableSpace)); // 19 - (19 - 6) == 6

                item.amount -= availableSpace; // Handling Overflow
                AddItem(item); // Continue the cycle
            } else
            {
                slot.AddItem(item.amount);
            }   
            return true;

        }

        return false;
    }
}
