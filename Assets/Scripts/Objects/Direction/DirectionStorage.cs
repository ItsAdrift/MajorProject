using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionStorage : MonoBehaviour
{
    public enum Direction { IDENTIFY, NORTH, SOUTH, EAST, WEST };

    private static IDictionary<Direction, Vector3> map = new Dictionary<Direction, Vector3>();

    private Vector3 NORTH = new Vector3(0, 0, 1);
    private Vector3 SOUTH = new Vector3(0, 0, -1);
    private Vector3 EAST = new Vector3(1, 0, 0);
    private Vector3 WEST = new Vector3(-1, 0, 0);

    public void Start()
    {
        map.Add(Direction.NORTH, NORTH);
        map.Add(Direction.SOUTH, SOUTH);
        map.Add(Direction.EAST, EAST);
        map.Add(Direction.WEST, WEST);
    }

    public static Vector3 Get(Direction dir)
    {
        return map[dir];
    }
}
