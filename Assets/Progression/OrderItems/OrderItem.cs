using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Order Item", menuName = "Progression/Order Item")]
public class OrderItem : ScriptableObject
{
    public ItemType type;
    [Range(1, 60)]
    public int min;

    [Range(1, 120)]
    public int max;
}
