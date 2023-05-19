using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRenderer : MonoBehaviour
{
    private Item item;
    [SerializeField] private Image image;
    void Start()
    {
        item = GetComponent<Item>();

        // If the image is not specified, attempt to find it in the object's children
        if (image == null)
            image = GetComponentInChildren<Image>();

        // Providing that the item is not null, set the item's render.
        if (item != null)
            Render();
    }

    void Render()
    {
        // Set the image's sprite (what it displays) to the sprite definined in ItemType
        image.sprite = item.type.render;
    }
}
