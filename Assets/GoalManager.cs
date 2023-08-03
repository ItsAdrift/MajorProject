using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public ProductionGoal[] goals;

    private void Start()
    {
        foreach(ProductionGoal goal in goals)
        {
            goal.progress = new GoalProgress();
        }
    }


    public void ItemProduced(ItemType type)
    {
        Debug.Log("Registering produced item: " + type.id);
        foreach (ProductionGoal goal in goals)
        {
            if (goal.type == type)
            {
                goal.progress.produced++;
                CheckGoalCompletion(goal);
            }
        }
    }

    public void ItemExported(ItemType type)
    {
        Debug.Log("Registering exported item: " + type.id);
        foreach (ProductionGoal goal in goals)
        {
            if (goal.type == type)
            {
                goal.progress.exported++;
                CheckGoalCompletion(goal);
            }
        }
    }

    public void CheckGoalCompletion(ProductionGoal goal)
    {
        if (goal.progress.produced >= goal.amountProduced && goal.progress.exported >= goal.amountExported)
        {
            // Goal is completed
            Debug.Log("Goal: " + goal.name + " completed, unlocking: " + goal.unlock.name);
            Unlock(goal.unlock);
        }
    }

    public void Unlock(ProductionUnlock unlock)
    {

    }
}
