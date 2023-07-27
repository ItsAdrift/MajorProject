using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwapper : MonoBehaviour
{
    public Material[] materials;

    private List<MeshRenderer> renderers = new List<MeshRenderer>();

    int cIndex = -1;

    public void Start()
    {
        foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
        {
            renderers.Add(mr);   
        }
    }

    public void Set(int index)
    {
        if (cIndex == index) return;

        cIndex = index;

        foreach(MeshRenderer mr in renderers)
        {
            mr.material = materials[index];
        }
    }
}
