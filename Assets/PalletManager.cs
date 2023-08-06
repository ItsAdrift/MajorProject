using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalletManager : MonoBehaviour
{
    public Pallet metalPallet;
    public Pallet chemicalPallet;
    public Pallet plasticPallet;
    public Pallet wirePallet;

    public ItemType metal;
    public ItemType chemical;
    public ItemType plastic;
    public ItemType wire;

    private Dictionary<Pallet, ItemType[]> palletContents = new Dictionary<Pallet, ItemType[]>();

    // Start is called before the first frame update
    void Start()
    {
        palletContents.Add(metalPallet, new ItemType[] { metal, metal, metal, metal });
        palletContents.Add(chemicalPallet, new ItemType[] { chemical, chemical, chemical, chemical });
        palletContents.Add(plasticPallet, new ItemType[] { plastic, plastic, plastic, plastic });
        palletContents.Add(wirePallet, new ItemType[] { wire, wire, wire, wire });

        FillAllPallets();
    }

    public void FillAllPallets()
    {
        FillPallet(metalPallet);
        FillPallet(chemicalPallet);
        FillPallet(plasticPallet);
        FillPallet(wirePallet);

    }

    public void ClearAllPallets()
    {
        metalPallet.ClearPallet();
        chemicalPallet.ClearPallet();
        plasticPallet.ClearPallet();
        wirePallet.ClearPallet();
    }

    public void RestockPallets()
    {
        int cost = GetRestockAllPalletsCost();

        if (GameManager.Instance.SubtractFunds(cost))
        {
            ClearAllPallets();
            FillAllPallets();
        }
    }

    public int GetRestockAllPalletsCost()
    {
        int count = 0;

        count += (metalPallet.GetEmptySlots() * metal.sellPrice);
        count += (chemicalPallet.GetEmptySlots() * chemical.sellPrice);
        count += (plasticPallet.GetEmptySlots() * plastic.sellPrice);
        count += (wirePallet.GetEmptySlots() * wire.sellPrice);

        return count;
    }

    public void FillPallet(Pallet pallet)
    {
        pallet.ClearPallet();
        pallet.AddItemTypes(palletContents.GetValueOrDefault(pallet));
    }
    
}
