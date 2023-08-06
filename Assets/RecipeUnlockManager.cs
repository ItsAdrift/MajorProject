using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeUnlockManager : MonoBehaviour
{
    [SerializeField] GameObject recipeUnlockUI;
    RecipeUnlockDisplay display;

    private List<MachineRecipe> queue = new List<MachineRecipe>();

    bool hasDisplay = false;

    private void Start()
    {
        display = recipeUnlockUI.GetComponent<RecipeUnlockDisplay>();
    }

    public void AddRecipeToQueue(MachineRecipe r)
    {
        if (!hasDisplay) { 
            queue.Add(r);
            return;
        }

        Display();
        hasDisplay = true;
    }

    public void Display()
    {
        recipeUnlockUI.SetActive(true);
        display.recipe = queue[0];
        display.Render();

        queue.RemoveAt(0);
    }

    public void Continue()
    {
        Debug.Log("Continue Button Clicked");
        if (queue.Count > 0)
        {
            Display();
        } else
        {
            recipeUnlockUI.SetActive(false);
            hasDisplay = false;
        }
    }
}
