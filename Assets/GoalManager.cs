using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public ProductionGoal[] goals;
    
    public Dictionary<ProductionGoal, GoalProgress> progress = new Dictionary<ProductionGoal, GoalProgress>();

    public List<ProductionGoal> unlockedGoals = new List<ProductionGoal>();

    private void Start()
    {
        foreach(ProductionGoal goal in goals)
        {
            Debug.Log("Added progress tracker for goal: " + goal.name);
            progress.Add(goal, new GoalProgress());
        }
    }


    public void ItemProduced(ItemType type)
    {
        Debug.Log("Registering produced item: " + type.id);
        foreach (ProductionGoal goal in goals)
        {
            if (goal.type == type)
            {
                GoalProgress gp = progress.GetValueOrDefault(goal);
                gp.produced++;
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
                GoalProgress gp = progress.GetValueOrDefault(goal);
                gp.exported++;
                CheckGoalCompletion(goal);
            }
        }
    }

    public void CheckGoalCompletion(ProductionGoal goal)
    {
        if (unlockedGoals.Contains(goal))
            return;

        GoalProgress gp = progress.GetValueOrDefault(goal);
        if (gp == null)
        {
            Debug.Log("Goal is null");
            return;
        }
        if (gp.produced >= goal.amountProduced && gp.exported >= goal.amountExported)
        {
            // Goal is completed
            Debug.Log("Goal: " + goal.name + " completed, unlocking: " + goal.unlock.name);
            Unlock(goal.unlock);
            unlockedGoals.Add(goal);
        }
    }

    public void Unlock(ProductionUnlock unlock)
    {
        GameManager.Instance.unlockedItems.AddRange(unlock.unlock);
        foreach (MachineRecipe r in unlock.recipeUnlock)
        {
            GameManager.Instance.recipeUnlockManager.AddRecipeToQueue(r);
            Debug.Log("Adding recipe: " + r.id + " to queue");
        }
        
    }
}
