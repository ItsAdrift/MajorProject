using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using static DirectionStorage;

public class ConveyorPlacementHandler : MonoBehaviour, IPlacementHandler
{
    public void handle(Entity e)
    {
        float rotY = e.transform.rotation.y;
        Conveyor conveyor = e.gameObject.GetComponent<Conveyor>();

        conveyor.direction = GetDirection(rotY);
    }

    private Direction GetDirection(float r)
    {
        switch (r)
        {
            case 0:
                return Direction.EAST;
            case 180:
                return Direction.WEST;
            case -90:
                return Direction.SOUTH;
            case 90:
                return Direction.NORTH;

            default: return Direction.NORTH;
        }
    }



}
