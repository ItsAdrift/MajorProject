using System.Collections;
using System.Collections.Generic;
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
                OrderManager.Instance.ItemExported(slot, slot.item.type);
            }
        }
    }
}
