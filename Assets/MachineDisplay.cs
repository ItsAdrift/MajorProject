using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineDisplay : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] GameObject holder;
    [SerializeField] Image[] images;

    private List<MachineDisplayPair> queue = new List<MachineDisplayPair>();

    public void AddItemTypesToQueue(MachineRecipe recipe, RecipeIngredient[] types)
    {
        queue.Add(new MachineDisplayPair(recipe.result, types));

        Display();
    } 

    private void Display()
    {
        holder.SetActive(true);
        SetFlashing(true);
        for (int i = 0; i < images.Length; i++)
        {
            if (queue[0].display[i] != null)
                images[i].sprite = queue[0].display[i].type.render; // item;
        }
    }

    public void OnItemProduced(ItemType type)
    {
        Debug.Log("Item produced: " + type.name + " (Machine Display)");
        MachineDisplayPair toRemove = null;

        foreach (MachineDisplayPair pair in queue)
        {
            if (pair.waitFor == type)
            {
                toRemove = pair; break;
            }
        }

        if (toRemove == null)
            return;
        
        queue.Remove(toRemove);

        if (queue.Count > 0)
            Display();
        else
            Disable();
    }

    private void Disable()
    {
        holder.SetActive(false);
        SetFlashing(false);
    }

    private void SetFlashing(bool b)
    {
        GetComponent<MachineIndicator>().enabled = true;
        GetComponent<MachineIndicator>().active = b;
    }
}
