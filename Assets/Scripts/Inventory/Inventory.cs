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
            if (slot.GetItem().type = item.type)
                return false;
            if (slot.GetAmount() < stack)
                return false;

            slot.AddItem();
            return true;

        }

        return false;
    }
}
