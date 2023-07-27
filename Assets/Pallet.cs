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
            GameObject parcel = Instantiate(Constants.Get().parcel);
            parcel.transform.position = slots[slot].transform.position;

            Item item = parcel.GetComponent<Item>();
            item.type = type;
            slots[slot].item = item;

            slot++;
        }
    }
}
