using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParcelGenerator : MonoBehaviour
{
    public GameObject parcelPrefab;
    ItemSlot slot;

    public bool itemCreated = false;

    // Start is called before the first frame update
    void Start()
    {
        slot = GetComponent<ItemSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (itemCreated)
            return;

        if (slot.item != null && slot.item.type != null)
        {
            GameObject parcel = Instantiate(parcelPrefab, transform);
            Item parcelItem = parcel.GetComponent<Item>();
            parcelItem.type = slot.item.type;

            ItemEntity itemEntity = parcel.GetComponent<ItemEntity>();
            itemEntity.slot = slot;

            itemCreated = true;
        }
    }

    public void _Reset()
    {
        itemCreated = false;
    }
}
