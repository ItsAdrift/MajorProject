using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public Item item;
    public bool hasItem;

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawCube(transform.position, new Vector3(0.9f,0.9f,0.9f));
    }

    public void ClearItem()
    {
        item = null;
        hasItem = false;
    }
}
