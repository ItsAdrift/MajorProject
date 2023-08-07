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

    private Dictionary<ItemType, MachineIndicator> machineDictionary = new Dictionary<ItemType, MachineIndicator>();

    [ReadOnly] public List<MachineIndicator> activeIndicators = new List<MachineIndicator>();

    public void Start()
    {
        orderManager = GetComponent<OrderManager>();
        recipeUnlockManager = GetComponent<RecipeUnlockManager>();

        orderManager.CreateOrder(stages[0].nextOrder.id);
        recipeUnlockManager.AddRecipeToQueue(firstRecipe);

        machineDictionary.Add(firstRecipe.result, fabrication);
        ActivateIndicator(fabrication);

        machineDictionary.Add(orderItems[0].type, workbench);
        machineDictionary.Add(orderItems[1].type, workbench);
        machineDictionary.Add(orderItems[2].type, workbench);
        machineDictionary.Add(orderItems[3].type, fabrication);
        machineDictionary.Add(orderItems[4].type, assembly);
        machineDictionary.Add(orderItems[5].type, fabrication);
    }

    public void OrderCompleted(ItemType type)
    {
        
        if (type == stages[4].nextOrder) {  // Game Device completed
            tutorial = false; // Tutorial Over, start generating random (& timed) order
            GameManager.Instance.gameTimer.active = true;
            ResetIndicators();
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

            if (machineDictionary.GetValueOrDefault(stages[stageIndex-1].nextOrder) == orderItems[1].type)
            {
                Debug.Log("Display/Circuit | Both should be flashing");
                ActivateIndicator(fabrication);
            } else
            {
                ResetIndicators();
            }
            ActivateIndicator(machineDictionary.GetValueOrDefault(stages[stageIndex].nextOrder));
        }
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

    private void ActivateIndicator(MachineIndicator indicator)
    { 
        EnableIndicator(indicator);
    }

    private void ResetIndicators()
    {
        foreach (MachineIndicator i in activeIndicators)
        {
            i._Reset();
            i.enabled = false;
        }
    }

    private void EnableIndicator(MachineIndicator indicator)
    {
        activeIndicators.Add(indicator);
        indicator.enabled = true;
    }

}
