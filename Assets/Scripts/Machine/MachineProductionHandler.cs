using System.Collections;
using System.Collections.Generic;
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
        machine.outputSlot.itemType = recipe.result;
        Destroy(this);
    }
}
