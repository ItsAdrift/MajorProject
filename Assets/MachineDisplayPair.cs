using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineDisplayPair
{
    public ItemType waitFor;

    public RecipeIngredient[] display;

    public MachineDisplayPair(ItemType waitFor, RecipeIngredient[] display)
    {
        this.waitFor = waitFor;
        this.display = display;
    }
}
