using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles targetting nearby objects. This could be machines, entities, etc.
public class PlayerTargetController : MonoBehaviour
{
    [SerializeField] float radius;

    [ReadOnly] public Entity targetedEntity;

    public void Update()
    {
        TargetEntity(FindClosest());
        if (!IsTargetedInRange())
        {
            ClearTarget();
        }
    }

    private bool IsTargetedInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider c in colliders) { 
            if (c.GetComponent<Entity>() == targetedEntity) return true;
        }

        return false;
    }

    public Entity FindClosest()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        Entity bestFit = null;
        float minSqrDistance = Mathf.Infinity;
        for (int i = 0; i < colliders.Length; i++)
        {
            float sqrDistanceToCenter = (transform.position - colliders[i].transform.position).sqrMagnitude;
            if (sqrDistanceToCenter < minSqrDistance)
            { 
                if (colliders[i].GetComponent<Entity>() != null)
                {
                    minSqrDistance = sqrDistanceToCenter;
                    bestFit = colliders[i].GetComponent<Entity>();
                }

            }
        }

        return bestFit;
    }

    private void TargetEntity(Entity e)
    {
        if (e == null)
            return;

        if (targetedEntity != null && targetedEntity != e)
        {
            targetedEntity.SetTargeted(false);
        }

        e.SetTargeted(true);
        targetedEntity = e;
    }

    private void ClearTarget()
    {
        targetedEntity.SetTargeted(false);
        targetedEntity = null;
    }
}
