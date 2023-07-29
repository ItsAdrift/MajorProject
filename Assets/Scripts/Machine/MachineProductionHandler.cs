using System.Collections;
using System.Collections.Generic;
using System.Net.Cache;
using UnityEngine;

public class MachineProductionHandler : MonoBehaviour
{
    Machine machine;
    MachineRecipe recipe;

    int count = 0;
    int time = 0;

    public void StartProduction(Machine machine, MachineTimer timer, MachineRecipe recipe)
    {
        this.machine = machine;
        this.recipe = recipe;

        time = recipe.processsingTime;

        // Start Timer for `time`
        timer.timerInterval.AddListener(Interval);

        // Start Production and remove used materials
        foreach (RecipeIngredient ingredient in recipe.ingredients)
        {
            foreach (ItemSlot slot in machine.itemSlots)
            {
                if (slot.item.type == ingredient.type)
                {
                    slot.item.amount -= ingredient.amount;
                    if (slot.item.amount <= 0)
                    {
                        Destroy(slot.item.gameObject);
                    }
                }
            }
        }
    }

    void Interval()
    {
        count++;
        if (count >= time)
        {
            FinishProduction();
        }
    }

    private void FinishProduction()
    {
        machine.outputSlot.item.type = recipe.result;
        foreach (ItemSlot slot in machine.itemSlots)
        {
            slot.hasItem = false;
            slot.item = null;
        }
        Destroy(this);
    }
}
