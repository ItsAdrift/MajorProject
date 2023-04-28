using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridAlignment : MonoBehaviour
{
    [SerializeField] private Vector3 alignment;
    [HideInInspector] private Vector3 offset;

    private void Awake()
    {
        offset = new Vector3(alignment.x, alignment.z, alignment.y);
    }

    public Vector3 GetOffset() { return offset; }

}
