using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Machine/Machine Type", fileName = "New Machine Type")]
public class MachineType : ScriptableObject
{
    public string name;
    public string id;
    public string description;

    public MachineRecipe[] recipes;
}
