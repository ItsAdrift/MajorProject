using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Placeable
    public bool placeable;
    public float rotation = 90f;
    public IPlacementHandler placementHandler;

    protected Outline outline;
    protected bool targeted = false;

    // Start is called before the first frame update
    void Awake()
    {
        outline = GetComponent<Outline>();
        if (outline == null)
            outline = gameObject.AddComponent<Outline>();


    }

    private void Update()
    {
        outline.SetState(targeted);
    }

    public void SetTargeted(bool b)
    {
        this.targeted = b;
    }

    public bool IsItem()
    {
        return gameObject.GetComponent<ItemEntity>() != null;
    }
}
