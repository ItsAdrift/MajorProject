using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outline : MonoBehaviour
{
    bool _enabled = false;

    string defaultLayer = "Default";

    private void Start()
    {
        defaultLayer = LayerMask.LayerToName(gameObject.layer);

        ModifyLayer(false);
    }

    public void SetState(bool state)
    {
        if (_enabled == state)
            return;

        _enabled = state;
        ModifyLayer(state);

    }

    private void ModifyLayer(bool b)
    {
        if (b)
            gameObject.layer = LayerMask.NameToLayer("Outline");
        else
            gameObject.layer = LayerMask.NameToLayer(defaultLayer);
    }


}
