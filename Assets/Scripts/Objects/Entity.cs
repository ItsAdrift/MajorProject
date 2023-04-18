using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Placeable
    public bool placeable;
    public float rotation = 90f;
    public IPlacementHandler placementHandler;

    private Outline outline;
    private bool targeted = false;

    // Start is called before the first frame update
    void Awake()
    {
        outline = gameObject.AddComponent<Outline>();
        if (outline != null)
        {
            outline.OutlineWidth = 5f;
            outline.enabled = false;
        }

        placementHandler = GetComponent<IPlacementHandler>();
    }

    private void Update()
    {
        outline.enabled = targeted;
    }

    public void SetTargeted(bool b)
    {
        this.targeted = b;
    }

}
