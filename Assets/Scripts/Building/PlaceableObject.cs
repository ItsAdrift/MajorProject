using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableObject : MonoBehaviour
{
    public bool placed {  get; private set; }
    public Vector3Int size { get; private set; }

    private Vector3[] verticies;

    private void GetColliderVertexPositionsLocal()
    {
        BoxCollider b = gameObject.GetComponent<BoxCollider>();
        verticies = new Vector3[4];
        verticies[0] = b.center + new Vector3(-b.size.x, -b.size.y, -b.size.z) * 0.5f;
        verticies[1] = b.center + new Vector3(b.size.x, -b.size.y, -b.size.z) * 0.5f;
        verticies[2] = b.center + new Vector3(b.size.x, -b.size.y, b.size.z) * 0.5f;
        verticies[3] = b.center + new Vector3(-b.size.x, -b.size.y, b.size.z) * 0.5f;
    }

    private void CalculateSizeInCells()
    {
        Vector3Int[] v = new Vector3Int[verticies.Length];

        for (int i = 0; i < v.Length; i++)
        {
            Vector3 worldPos = transform.TransformPoint(verticies[i]);
            v[i] = BuildingSystem.instance.gridLayout.WorldToCell(worldPos);
        }

        size = new Vector3Int(Mathf.Abs((v[0] - v[1]).x), Mathf.Abs((v[0] - v[3]).y), 1);
    }

    public Vector3 GetStartPosition()
    {
        return transform.TransformPoint(verticies[0]);
    }

    private void Awake()
    {
        GetColliderVertexPositionsLocal();
        CalculateSizeInCells();
    }

    public void Rotate()
    {
        size = new Vector3Int(size.y, size.x, 1);

        Vector3[] v = new Vector3[verticies.Length];
        for (int i = 0; i < verticies.Length; i++)
        {
            v[i] = verticies[(i + 1) % verticies.Length];
        }

        verticies = v;
    }

}
