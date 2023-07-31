using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Machine : MonoBehaviour
{
    [SerializeField] public MachineType type;
    [SerializeField] public ItemSlot[] itemSlots;

    [SerializeField] public ItemSlot outputSlot;

    [SerializeField] public Slider slider;

    private MachineTimer timer;
    private MachineProductionHandler productionHandler;

    public void Start()
    {
        // Machine Timer
        if (!GetComponent<MachineTimer>())
            timer = gameObject.AddComponent<MachineTimer>();
        else
            timer = GetComponent<MachineTimer>();

        timer.timerInterval.AddListener(Interval);
    }

    // This logic runs every second
    public void Interval()
    {
        if (HasRequiredItemsForRecipe(GetSelectedRecipe()))
        {
            // Create & Assign a new production handler
            productionHandler = gameObject.AddComponent<MachineProductionHandler>();

            productionHandler.StartProduction(this, timer, GetSelectedRecipe());
        }   
    }

    /*
     * This method currently does nothing; however when recipe selection is supported,
     * this method will be responsible for returing the correct recipe.
     */
    public MachineRecipe GetSelectedRecipe()
    {
        return type.recipes[0];
    }

    private bool HasRequiredItemsForRecipe(MachineRecipe recipe)
    {
        if (recipe == null) return false;

        int count = 0;

        for (int i = 0; i < recipe.ingredients.Length; i++) { 
            if (recipe.ingredients[i] == null) continue;
            if (recipe.ingredients[i].type == null) continue;

            bool anySlotHasItem = false;
            for (int j = 0; j < itemSlots.Length; j++)
            {
                if (itemSlots[j].item == null) continue;

                if (recipe.ingredients[i].type == itemSlots[j].item.type)
                {
                    anySlotHasItem = true;
                }
            }

            if (anySlotHasItem)
                count++;
        }

        return (count >= recipe.ingredients.Length);
    }

}
