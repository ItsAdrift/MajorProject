using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExportPallet : Pallet
{
    GlobalTimer timer;

    private void Start()
    {
        timer = GlobalTimer.instance;
        timer.timerInterval.AddListener(Interval);
    }

    void Interval()
    {
        foreach (ItemSlot slot in slots)
        {
            if (slot.hasItem)
            {
                ItemType type = slot.item.type;
                if (OrderManager.Instance.ItemExported(slot, slot.item.type))
                {
                    GameManager.Instance.goalManager.ItemExported(type);
                }
            }
        }
    }
}
