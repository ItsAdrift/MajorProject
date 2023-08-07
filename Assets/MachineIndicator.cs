using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineIndicator : MonoBehaviour
{
    public Material materialA;
    public Material materialB;

    public MeshRenderer meshRenderer;

    public float delay = 1;

    private float time;
    private bool b = false;

    public void Update()
    {
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
    }
}
