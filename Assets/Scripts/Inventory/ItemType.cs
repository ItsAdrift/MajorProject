using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Item Type", fileName = "New Item Type")]
public class ItemType : ScriptableObject
{
    public string id;
    public Sprite render;

    public int sellPrice;
}
