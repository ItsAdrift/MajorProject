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
        foreach (MachineRecipe r in type.recipes)
        {
          
            if (HasRequiredItemsForRecipe(r))
            {
               
                // Create & Assign a new production handler
                productionHandler = gameObject.AddComponent<MachineProductionHandler>();

                productionHandler.StartProduction(this, timer, r); // This was causing an issue where only the first recipe was being produced. `r` was previously left as GetSelectedRecipe(); meaning it would never start producing the correct component
            }
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

        List<KeyValuePair<ItemType, bool>> ingredients = new List<KeyValuePair<ItemType, bool>>();
        foreach (RecipeIngredient ri in recipe.ingredients)
        {
            ingredients.Add(new KeyValuePair<ItemType, bool>(ri.type, false));
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].item == null) continue;
            if (itemSlots[i].item.type == null) continue;

            ItemType type = itemSlots[i].item.type;
            bool ingredientFound = false;

            for (int ingredientsIndex = 0; ingredientsIndex < ingredients.Count; ingredientsIndex++)
            {
                if (!ingredients[ingredientsIndex].Value && ingredients[ingredientsIndex].Key.id == type.id)
                {
                    ingredientFound = true;
                    break;
                }
            }

            if (ingredientFound)
            {
                for (int ingredientsIndex = 0; ingredientsIndex < ingredients.Count; ingredientsIndex++)
                {
                    if (ingredients[ingredientsIndex].Key.id == type.id)
                        ingredients[ingredientsIndex] = new KeyValuePair<ItemType, bool>(type, true);
                }
            }
        }

        bool hasRequired = false;
        int count = 0;

        foreach(KeyValuePair<ItemType, bool> kvp in ingredients)
        {
            if (kvp.Value)
                count++;
        }
        hasRequired = count >= ingredients.Count;
        return hasRequired;

        /*for (int i = 0; i < recipe.ingredients.Length; i++) { 
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

        return (count >= recipe.ingredients.Length);*/
    }

}
