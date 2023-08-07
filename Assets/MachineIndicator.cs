using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineIndicator : MonoBehaviour
{
    public Material materialA;
    public Material materialB;

    public MeshRenderer meshRenderer;

    public float delay = 1;

    public bool active = false;

    private float time;
    private bool b = false;

    private void Start()
    {
        GlobalTimer.instance.timerInterval.AddListener(Swap);
    }

    /*
     * public void Update()
    {
        if (!active)
        {
            meshRenderer.material = materialA;
            return;
        }
            

        time += Time.deltaTime;

        if (time > delay )
        {
            time = 0;
            b = !b;
            // Do stuff
            if ( b )
                meshRenderer.material = materialA;
            else
                meshRenderer.material = materialB;
        }
    }*/

    public void Swap()
    {
        if (!active)
        {
            meshRenderer.material = materialA;
            return;
        }

        // Do stuff
        if (GlobalTimer.instance.indicatorState)
            meshRenderer.material = materialA;
        else
            meshRenderer.material = materialB;
    }

    public void _Reset()
    {
        meshRenderer.material = materialA;
    }
}
