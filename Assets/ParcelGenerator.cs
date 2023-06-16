using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParcelGenerator : MonoBehaviour
{
    public GameObject parcelPrefab;
    ItemSlot slot;

    bool b = false;

    // Start is called before the first frame update
    void Start()
    {
        slot = GetComponent<ItemSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (b)
            return;

        if (slot.item.type != null)
        {
            GameObject parcel = Instantiate(parcelPrefab, transform);
            Item parcelItem = parcel.GetComponent<Item>();
            parcelItem.type = slot.item.type;
            b = true;
        }
    }
}
