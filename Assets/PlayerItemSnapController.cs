using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerItemSlotTargettingController), typeof(PlayerObjectController))]
public class PlayerItemSnapController : MonoBehaviour
{
    [Header("Required Scripts")]
    [ReadOnly] public PlayerItemSlotTargettingController targettingController;
    [ReadOnly] public PlayerObjectController playerObjectController;

    // Start is called before the first frame update
    void Start()
    {
        targettingController = GetComponent<PlayerItemSlotTargettingController>();
        playerObjectController = GetComponent<PlayerObjectController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObjectController.heldEntity != null)
        {
            if (playerObjectController.heldEntity.IsItem())
            {
                if (targettingController.target != null)
                {
                    playerObjectController.heldItemCopy.transform.position = targettingController.target.transform.position;

                    // Ensure that it is not in a position that another parcel is already in.
                    /*Collider[] colliders = Physics.OverlapSphere(playerObjectController.heldItemCopy.transform.position, 1);

                    foreach (Collider c in colliders)
                    {
                        if (GameObject.ReferenceEquals(c.gameObject, playerObjectController.heldItemCopy))
                        {
                            Debug.Log("Same");
                            continue;
                        }
                        if (c.gameObject.activeSelf)
                        {
                            Debug.Log("Hit active parcel!");
                        }
                    }*/

                    playerObjectController.heldItemCopy.GetComponent<MaterialSwapper>().Set(1);
                    playerObjectController.heldItemCopy.SetActive(true);
                    playerObjectController.heldEntity.gameObject.SetActive(false);

                    ItemEntity itemEntity = playerObjectController.heldItemCopy.GetComponent<ItemEntity>();
                    itemEntity.snapped = true;
                    itemEntity.slot = targettingController.target;
                }
                else
                {
                    playerObjectController.heldItemCopy.SetActive(false);
                    playerObjectController.heldEntity.gameObject.SetActive(true);

                    ItemEntity itemEntity = playerObjectController.heldItemCopy.GetComponent<ItemEntity>();
                    itemEntity.snapped = false;
                    itemEntity.slot = null;
                }
            }
        }
        
        
    }
}
