using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Production Unlock", menuName = "Production/Production Unlock")]
public class ProductionUnlock : ScriptableObject
{
    public ItemType[] unlock;
}
