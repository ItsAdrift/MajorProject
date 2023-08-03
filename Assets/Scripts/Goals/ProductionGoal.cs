using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Production Goal", menuName = "Production/Production Goal")]
public class ProductionGoal : ScriptableObject
{
    [Header("Goal Requirements")]
    public ItemType type;
    public int amountProduced;
    public int amountExported;

    [Header("Reward/Unlock")]
    public ProductionUnlock unlock;

    [Header("Runtime")]
    [ReadOnly] public GoalProgress progress;
    
}
