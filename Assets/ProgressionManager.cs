using JetBrains.Annotations;
using System.Collections.Generic;
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
    public int additionalOrderChance = 10;

    OrderManager orderManager;
    RecipeUnlockManager recipeUnlockManager;

    [Header("Benches")]
    public MachineIndicator fabrication;
    public MachineIndicator workbench;
    public MachineIndicator assembly;

    public MachineDisplay fabricationDisplay;
    public MachineDisplay workbenchDisplay;
    public MachineDisplay assemblyDisplay;

    [ReadOnly] public Dictionary<MachineRecipe.Machine, MachineDisplay> displays = new Dictionary<MachineRecipe.Machine, MachineDisplay>();

    public void Start()
    {
        orderManager = GetComponent<OrderManager>();
        recipeUnlockManager = GetComponent<RecipeUnlockManager>();

        orderManager.CreateOrder(stages[0].nextOrder.id);
        recipeUnlockManager.AddRecipeToQueue(firstRecipe);

        displays.Add(MachineRecipe.Machine.Fabrication_Machine, fabricationDisplay);
        displays.Add(MachineRecipe.Machine.Workbench, workbenchDisplay);
        displays.Add(MachineRecipe.Machine.Assembly_Machine, assemblyDisplay);

        DisplayRecipeOnMachine(orderItems[0]);

    }

    public void OrderCompleted(ItemType type)
    {
        
        if (type == stages[4].nextOrder) {  // Game Device completed
            tutorial = false; // Tutorial Over, start generating random (& timed) order
            GameManager.Instance.gameTimer.active = true;
        }
        if (!tutorial)
        {
            CreateRandomOrder();
            return;
        }
        
        if (stageIndex < stages.Length && stages[stageIndex].nextOrder == type)
        {
            stageIndex++;
            orderManager.CreateOrder(stages[stageIndex].nextOrder.id);

            DisplayRecipeOnMachine(Lookup(stages[stageIndex].nextOrder));
        }
    }

    private OrderItem Lookup(ItemType type)
    {
        foreach (OrderItem item in orderItems) {
            if (item.type == type) return item;
        }

        return null;
    }

    public void CreateRandomOrder()
    {
        int amountOfOrders = 1;

        int outstandingOrders = orderManager.activeOrders.Count;
        if (outstandingOrders + 1 < maxOrders && Random.Range(0, 100) < additionalOrderChance)
        {
            amountOfOrders++;
        }


        GenerateOrders(amountOfOrders);
    }

    private void GenerateOrders(int amount)
    {
       for (int i = 0; i < amount; i++)
        {
            List<OrderItem> oi = new List<OrderItem>();
            oi.AddRange(orderItems);

            foreach (Order order in orderManager.activeOrders)
            {
                if (order.type == orderItems[4].type)
                {
                    oi.Remove(orderItems[4]);
                    // Has a game device don't make another
                }
            }

            OrderItem item = oi[Random.Range(0, oi.Count)];
            
            orderManager.CreateOrder(item.type.id, Random.Range(item.min, item.max + 1));
        }
    }

    public void DisplayRecipeOnMachine(OrderItem item)
    {
        Debug.Log("Displaying");
        foreach (MachineRecipe r in item.requiredRecipes)
        {
            MachineDisplay display = displays.GetValueOrDefault(r.producedIn);
            if (display != null)
            {
                // Add items to queue
                display.AddItemTypesToQueue(r, r.ingredients);
            }

        }

    }

}
