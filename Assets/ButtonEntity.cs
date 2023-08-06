using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonEntity : Entity
{
    public TMP_Text text;

    public void ButtonPress()
    {
        Debug.Log("Handling Restock");
        GameManager.Instance.palletManager.RestockPallets();
    }

    private void Update()
    {
        text.text = ""+GameManager.Instance.palletManager.GetRestockAllPalletsCost();

        outline.SetState(targeted);
    }
}
