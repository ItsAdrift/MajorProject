using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Machine/Recipe Ingredient", fileName = "New Machine Ingredient")]
public class RecipeIngredient : ScriptableObject
{
    public ItemType type;
    public int amount;
}
