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
        if (targettingController.target != null && playerObjectController.heldEntity != null)
        {
            if (playerObjectController.heldEntity.IsItem())
            {
                playerObjectController.heldItemCopy.transform.position = targettingController.target.transform.position;
                playerObjectController.heldItemCopy.GetComponent<MaterialSwapper>().Set(1);
                playerObjectController.heldItemCopy.SetActive(true);
                playerObjectController.heldEntity.gameObject.SetActive(false);
            }
        } 
    }
}
