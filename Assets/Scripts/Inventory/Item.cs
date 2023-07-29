using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemType type;
    public int amount = 1;

    public void _Reset()
    {
        type = null;
    }
}
