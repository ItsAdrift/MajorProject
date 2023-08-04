using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Machine/Machine Recipe", fileName = "New Machine Recipe")]
public class MachineRecipe : ScriptableObject
{
    public string name;
    public string id;

    public int processsingTime;
    public RecipeIngredient[] ingredients;

    public ItemType result;

    public enum Machine
    {
        Fabrication_Machine,
        Workbench,
        Assembly_Machine
    }

    public Machine producedIn;
}
