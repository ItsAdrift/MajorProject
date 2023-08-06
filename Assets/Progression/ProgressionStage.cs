using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Progression Stage", menuName = "Progression/Progression Stage")]
public class ProgressionStage : ScriptableObject
{
    public ItemType completedOrder;

    public ItemType nextOrder;
}
