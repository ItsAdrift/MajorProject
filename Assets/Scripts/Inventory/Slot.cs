using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot
{
    private Item item;
    private int amount = 0;

    public Item GetItem()
    {
        return item;
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public int GetAmount()
    {
        return amount;
    }

    public void AddItem()
    {
        amount++;
    }

    public void AddItem(int i)
    {
        amount+=i;
    }

    public void RemoveItem()
    {
        if (amount == 0)
            return;
        amount--;
    }
}
