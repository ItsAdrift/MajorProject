using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        List<Vector3> slots = new List<Vector3>();

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        ItemSlot slot = null;

        float minSqrDistance = Mathf.Infinity;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag != "ItemSlot")
                continue;

            float sqrDistanceToCenter = (transform.position - colliders[i].transform.position).sqrMagnitude;
            if (sqrDistanceToCenter < minSqrDistance)
            { 
                if (colliders[i].GetComponent<ItemSlot>() != null)
                {
                    //ItemSlot s = colliders[i].GetComponent<ItemSlot>();
                    //if (s.hasItem)
                        //continue;

                    //slot = s;
                    //minSqrDistance = sqrDistanceToCenter;
                    //slots.Add(colliders[i].transform.position);
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
