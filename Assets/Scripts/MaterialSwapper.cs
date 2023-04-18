using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSwapper : MonoBehaviour
{
    public Material[] materials;

    private List<MeshRenderer> renderers = new List<MeshRenderer>(); 

    public void Start()
    {
        foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
        {
            renderers.Add(mr);   
        }
    }

    public void Set(int index)
    {
        foreach(MeshRenderer mr in renderers)
        {
            mr.material = materials[index];
        }
    }
}
