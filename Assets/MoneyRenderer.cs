using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyRenderer : MonoBehaviour
{
    public TMP_Text text;
    public Money money;

    private void LateUpdate()
    {
        text.text = money.money.ToString();
    }
}
