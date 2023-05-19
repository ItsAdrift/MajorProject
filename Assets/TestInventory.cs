using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventory : MonoBehaviour
{
    private Inventory inv;

    public int createAmount = 9;
    public GameObject itemPrefab;

    public List<ItemType> itemTypes = new List<ItemType>();

    private void Start()
    {
        inv = GetComponent<Inventory>();

        if (createAmount > inv.size)
        {
            Debug.LogWarning("createAmount is set higher than Inventory#size; setting createAmount to max");
            createAmount = inv.size;
        }

        for (int i = 0; i < createAmount; i++)
        {
            GameObject itemObject = Instantiate(itemPrefab);
            Item item = itemObject.GetComponent<Item>();
            
            item.type = itemTypes[Random.Range(0, itemTypes.Count)];
            
            item.amount = Random.Range(1, inv.stack+1);

            Debug.LogWarning("Info " + i + ": " + item.type + " | Amount: " + item.amount);

            if (inv.AddItem(item)) {
                Debug.Log("Successfully Added Item");
            } else
            {
                Debug.LogError("Unable to add item");
            }
        }

        Debug.Log("Testing Inventory");

        for (int i = 0; i < inv.size; i++)
        {
            bool filled = inv.slots[i].GetItem() != null;
            string type = "undefined";
            int amount = -1;

            if (filled) { 
                Item item = inv.slots[i].GetItem();
                type = item.type.id;
                amount = item.amount;
            }

            Debug.Log("Filled: " + filled + " | Type: " + type + " | Amount: " + amount);
        }

    }
}
