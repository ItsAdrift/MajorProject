using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class RecipeUnlockDisplay : MonoBehaviour
{
    public MachineRecipe recipe;

    [Header("Fields")]
    public Image itemIcon;
    public TMP_Text itemName;
    public TMP_Text sellPrice;

    public TMP_Text madeIn;

    [Header("Ingredients")]
    public GameObject[] ingredientContainers;
    public Image[] ingredients1;
    public Image[] ingredients2;
    public Image[] ingredients3;

    List<Image[]> ingredients = new List<Image[]>();

    private void Start()
    {
        ingredients.AddRange(new Image[][] { ingredients1, ingredients2, ingredients3 });

        Render();
    }

    public void Render()
    {
        itemIcon.sprite = recipe.result.render;

        itemName.text = recipe.result.name;
        sellPrice.text = ""+recipe.result.sellPrice;

        int ingredientAmount = recipe.ingredients.Length -1;
        EnableIngredientContainer(ingredientAmount);

        for (int i = 0; i < recipe.ingredients.Length; i++)
        {
            ingredients[ingredientAmount][i].sprite = recipe.ingredients[i].type.render;
        }

        madeIn.text = "Made In: " + recipe.producedIn.ToString().Replace("_", " ");
    }

    private void EnableIngredientContainer(int ingredientAmount)
    {
        foreach(GameObject obj in ingredientContainers)
        {
            obj.SetActive(false);
        }

        ingredientContainers[ingredientAmount].SetActive(true);
    }
}
