using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerObjectController : MonoBehaviour
{
    [SerializeField] public GameObject objectHold;
    [SerializeField] public GameObject itemHold;

    [SerializeField] public GameObject precisionHold;

    [ReadOnly] public Entity heldEntity;

    private PlayerTargetController targetController;
    private PlayerBuilding building;

    [HideInInspector] public GameObject heldItemCopy;

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
        if (heldEntity.IsItem() && !heldEntity.placeable)
        {
            PlaceItem();
            return;
        }

        if (heldEntity.placeable)
        {
            building.Place();
            targetController.enabled = true;
            return;
        }

        heldEntity.transform.SetParent(null);
        heldEntity = null;
    }

    public void PlaceItem()
    {
        if (heldItemCopy.GetComponent<ItemEntity>().snapped)
        {
            ItemEntity entity = heldItemCopy.GetComponent<ItemEntity>();
            Item item = heldEntity.GetComponent<Item>();
            
            heldEntity.GetComponent<ItemEntity>().slot = entity.slot;

            Debug.Log("Type: " + item.type.name + " | Amount: " + item.amount);

            entity.slot.item = item;
            entity.slot.hasItem = true;

            heldEntity.GetComponent<ItemEntity>().snapped = false;
            heldEntity.gameObject.SetActive(true);
            heldEntity.transform.position = heldItemCopy.transform.position;
            Destroy(heldItemCopy);


        }
        targetController.enabled = true;
        heldEntity.transform.SetParent(null);
        heldEntity = null;
        //Destroy(heldItemCopy); // added this to stop freezes maybe)
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

            Transform t = heldEntity.gameObject.GetComponent<ItemEntity>() != null ? itemHold.transform : objectHold.transform;

            if (heldEntity.placeable)
                building.HandleEntity(heldEntity);
            else if (heldEntity.IsItem())
            {
                heldItemCopy = building.CreateCopy(heldEntity, new Vector3(-90f, 0, 0)); // This causes a freeze
                if (heldEntity.GetComponent<ItemEntity>().slot != null)
                {
                    //heldEntity.GetComponent<ItemEntity>().slot.item = null;
                    heldEntity.GetComponent<ItemEntity>().slot.hasItem = false;

                    heldEntity.GetComponent<ItemEntity>().slot.gameObject.GetComponent<Item>()?._Reset();
                    heldEntity.GetComponent<ItemEntity>().slot.gameObject.GetComponent<ParcelGenerator>()?._Reset();

                    if (heldEntity.transform.parent != null && heldEntity.transform.parent.parent != null)
                    {
                        Slider s = heldEntity.GetComponent<ItemEntity>().transform.parent.parent.GetComponentInChildren<Slider>();
                        if (s != null)
                        {
                            s.value = 0f;
                        }
                    }  
                }
            }
            o.transform.SetParent(t);

            o.transform.localPosition = Vector3.zero;
            o.transform.localRotation = Quaternion.Euler(Vector3.zero);

            targetController.enabled = false;
        }
    }
}
