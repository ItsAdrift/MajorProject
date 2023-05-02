using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem instance;

    [SerializeField] private float checkRadius = 0.5f;

    [Space]

    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap mainTilemap;
    [SerializeField] private TileBase whiteTileBase;

    public GameObject prefab;

    public PlaceableObject objectToPlace;

    #region Unity methods

    private void Awake()
    {
        instance = this;
        grid = gridLayout.GetComponent<Grid>();
    }
    #endregion

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        return position;
    }

    public Vector3 SnapEntityCoordinateToGrid(Vector3 position, GridAlignment align)
    {
        Vector3Int cellPos = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPos);
        if (align != null)
            position += align.GetOffset();
        return position;
    }

    public bool CanBePlaced(PlaceableObject placeableObject)
    {
        Collider[] colliders = Physics.OverlapSphere(placeableObject.transform.position, checkRadius);
        foreach (Collider c in colliders) 
        {
            if (c.gameObject.tag != "Placeable")
                continue; // detected something other than another placeable

            if (placeableObject.gameObject.GetInstanceID() == c.gameObject.GetInstanceID())
                continue; // detected self

            if (c.bounds.Intersects(placeableObject.GetComponent<Collider>().bounds))
                return false;
        }

        return true;
    }

    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        mainTilemap.BoxFill(start, whiteTileBase, start.x, start.y, start.x + size.x, start.y + size.y);
    }

}
