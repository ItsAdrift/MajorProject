using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerObjectController : MonoBehaviour
{
    [SerializeField] public GameObject objectHold;
    [SerializeField] public GameObject itemHold;

    [SerializeField] public GameObject precisionHold;

    [ReadOnly] public Entity heldEntity;

    private PlayerTargetController targetController;
    private PlayerBuilding building;

    private void Start()
    {
        targetController = GetComponent<PlayerTargetController>();
        building = GetComponent<PlayerBuilding>();
    }


    public void onPickUpPlace(InputAction.CallbackContext c)
    {
        if (c.phase != InputActionPhase.Started)
            return;

        if (heldEntity != null)
        {
            Place();
        } else
        {
            Pickup();
        }
    }

    public void Rotate(InputAction.CallbackContext c)
    {
        if (c.phase != InputActionPhase.Performed)
            return;

        if (heldEntity != null)
        {
            heldEntity.rotation += 90f;
            building.HandleRotate();
        }
    }

    public void Place()
    {
        if (heldEntity.placeable)
        {
            building.Place();
            targetController.enabled = true;
            return;
        }

        heldEntity.transform.SetParent(null);
        heldEntity = null;
    }

    public void Pickup()
    {
        if (heldEntity != null)
            return;

        if (targetController == null)
            return;

        if (targetController.targetedEntity != null)
        {
            heldEntity = targetController.targetedEntity;
            GameObject o = targetController.targetedEntity.gameObject;

            Transform t = heldEntity is ItemEntity ? itemHold.transform : objectHold.transform;

            o.transform.SetParent(t);


            o.transform.localPosition = Vector3.zero;
            o.transform.localRotation = Quaternion.Euler(Vector3.zero);
            if (heldEntity.placeable)
                building.HandleEntity(heldEntity);
            targetController.enabled = false;
        }
    }
}
