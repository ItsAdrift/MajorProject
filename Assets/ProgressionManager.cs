using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    [Header("Stages")]
    [ReadOnly] public string comment = "Index 0 will be provided as a recipe on game start";

    [Tooltip("Index 0 will be provided as a recipe on game start")]
    public MachineRecipe firstRecipe;
    public ProgressionStage[] stages;

    OrderManager orderManager;
    RecipeUnlockManager recipeUnlockManager;
    
    public void Start()
    {
        orderManager = GetComponent<OrderManager>();
        recipeUnlockManager = GetComponent<RecipeUnlockManager>();

        orderManager.CreateOrder(stages[0].nextOrder.id);
        recipeUnlockManager.AddRecipeToQueue(firstRecipe);
    }

    public void OrderCompleted(ItemType type)
    {
        foreach (ProgressionStage stage in stages)
        {
            if (stage.completedOrder == type)
            {
                orderManager.CreateOrder(stage.nextOrder.id);
            }
        }
    }

}
