using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemSlotTargettingController : MonoBehaviour
{
    [SerializeField] float radius;

    [ReadOnly] public ItemSlot target;

    void Update()
    {
        TargetSlot(FindClosest()); // Find the closest valid ItemSlot and target it.
        
    }

    // Find the closest ItemSlot to the player
    public ItemSlot FindClosest()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        ItemSlot slot = null;

        float minSqrDistance = Mathf.Infinity;
        for (int i = 0; i < colliders.Length; i++)
        {
            float sqrDistanceToCenter = (transform.position - colliders[i].transform.position).sqrMagnitude;
            if (sqrDistanceToCenter < minSqrDistance)
            {
                minSqrDistance = sqrDistanceToCenter;

                if (colliders[i].GetComponent<ItemSlot>() != null)
                {
                    slot = colliders[i].GetComponent<ItemSlot>();
                }
            }
        }

        return slot;
    }

    public void TargetSlot(ItemSlot slot)
    {
        target=slot;
    }
}
