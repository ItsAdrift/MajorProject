using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DirectionStorage;
using static Constants;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] public Direction direction;

    public ConveyorBelt next;
    public ConveyorItem item;

    bool moved = false;

    // Start is called before the first frame update
    void Start()
    {
        if (direction == Direction.IDENTIFY)
        {
            direction = GetDirection(transform.rotation.y);
        }

        FindNext();
    }

    public void FindNext()
    {
        // Set the raycast origin position and direction
        Vector3 raycastOrigin = transform.position; // Use the position of the object that the script is attached to as the origin
        raycastOrigin -= new Vector3(0, 0.1f, 0);
        Vector3 raycastDirection = transform.forward; // Use the forward direction of the object that the script is attached to as the direction

        // Set the maximum distance that the raycast can travel
        float raycastDistance = 1.5f;

        // Perform the raycast and get the hit information
        RaycastHit hit;
        Debug.DrawRay(raycastOrigin, raycastDirection, Color.green, 5);
        if (Physics.Raycast(raycastOrigin, raycastDirection, out hit, raycastDistance))
        {
            next = hit.collider.gameObject.GetComponent<ConveyorBelt>();
        }
    }


    public void ResetMove()
    {
        moved = false;
        elapsedTime = 0;
}

    public void AttemptMove()
    {
        // Check if there is an item on the conveyor belt
        if (item != null)
        {
            // Calculate the new position of the item based on the conveyor belt's direction and speed
            //Vector3 newItemPos = item.transform.position + (Get(direction) * Constants.GetInstance().conveyorSpeed * Time.deltaTime);

            // Check if there is enough space on the next conveyor belt for the item
            if (!moved && next != null && next.item == null)
            {
                // Move the item to the next conveyor belt
                next.item = item;
                item = null;
                next.moved = true;
            }
        }
    }

    private float moveDuration = 1f;

    private float elapsedTime = 0f;

    private void Update()
    {
        moveDuration = Get().conveyorSpeed;
        if (item != null)
        {
            if (elapsedTime < moveDuration)
            {
                float t = elapsedTime / moveDuration;
                Vector3 pos = Vector3.Lerp(item.transform.position, transform.position, t);
                pos.y = Get().conveyorHeight;
                item.transform.position = pos;
                elapsedTime += Time.deltaTime;
            }
            
        }
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
