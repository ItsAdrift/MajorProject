using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public Item item;

    public void Awake()
    {
        if (item != null)
            return;
        if (gameObject.GetComponent<Item>() != null)
            item = gameObject.GetComponent<Item>();
        else { 
            gameObject.AddComponent<Item>();
            item = gameObject.GetComponent<Item>();
        }
    }
}
