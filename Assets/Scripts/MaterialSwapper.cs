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
        //if (cIndex == index) return;

        //cIndex = index;

        int changeMatFor = 2;

        foreach(MeshRenderer mr in renderers)
        {
            List<Material> l = new List<Material>();
            for (int i = 0; i < changeMatFor; i++)
            {
                l.Add(materials[index]);
            }

            //mr.material = materials[index];
            mr.SetMaterials(l);
        }
    }
}
