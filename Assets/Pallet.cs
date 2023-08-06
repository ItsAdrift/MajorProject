using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pallet : MonoBehaviour
{
    public ItemSlot[] slots;

    public void AddItemTypes(ItemType[] types)
    {
        int slot = 0;
        foreach (ItemType type in types)
        {
            GameObject parcel = Instantiate(Constants.Get().parcel, slots[slot].transform);
            parcel.transform.position = slots[slot].transform.position;

            Item item = parcel.GetComponent<Item>();
            item.type = type;
            slots[slot].item = item;

            slot++;
        }
    }
     
    public void ClearPallet()
    {
        foreach (ItemSlot slot in slots)
        {
            if (slot.transform.childCount > 0)
                Destroy(slot.transform.GetChild(0).gameObject);
        }
    }

    

    public int GetEmptySlots()
    {
        int count = 0;

        foreach (ItemSlot slot in slots)
        {
            if (slot.transform.childCount == 0)
                count++;
        }

        return count;
    }
}
