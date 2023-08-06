using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    [Header("Stages")]
    [ReadOnly] public string comment = "Index 0 will be provided as a recipe on game start";

    [Tooltip("Index 0 will be provided as a recipe on game start")]
    public MachineRecipe firstRecipe;
    public ProgressionStage[] stages;
    [ReadOnly] public int stageIndex = 0;

    [Header("Post-Tutorial")]
    [ReadOnly] public bool tutorial = true;
    [Space]
    public OrderItem[] orderItems;

    [Header("Additional Orders")]
    public int maxOrders = 3;
    public int additionalOrderChance = 25;

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
        if (type == stages[4].nextOrder) // Game Device completed
            tutorial = false; // Tutorial Over, start generating random (& timed) order

        if (!tutorial)
        {
            CreateRandomOrder();
            return;
        }
        
        if (stageIndex < stages.Length && stages[stageIndex].nextOrder == type)
        {
            stageIndex++;
            orderManager.CreateOrder(stages[stageIndex].nextOrder.id);
        }
    }

    public void CreateRandomOrder()
    {
        int amountOfOrders = 1;

        int outstandingOrders = orderManager.activeOrders.Count;
        if (outstandingOrders + 1 < maxOrders && Random.Range(0, 100) > additionalOrderChance)
        {
            amountOfOrders++;
        }


        GenerateOrders(amountOfOrders);
    }

    private void GenerateOrders(int amount)
    {
       for (int i = 0; i < amount; i++)
        {
            OrderItem item = orderItems[Random.Range(0, orderItems.Length)];
            orderManager.CreateOrder(item.type.id, Random.Range(item.min, item.max + 1));
        }
    }

}
