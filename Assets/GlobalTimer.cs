using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTimer : MonoBehaviour
{
    public float interval = 1f; // The interval in seconds between each update
    private float timer = 0f; // The timer that counts up to the interval
    private List<ConveyorBelt> conveyors = new List<ConveyorBelt>(); // A list of all the conveyors in the scene

    void Start()
    {
        // Find all the conveyors in the scene and add them to the list
        ConveyorBelt[] conveyorArray = FindObjectsOfType<ConveyorBelt>();
        foreach (ConveyorBelt conveyor in conveyorArray)
        {
            conveyors.Add(conveyor);
        }
    }

    void Update()
    {
        // Increment the timer
        timer += Time.deltaTime;

        // Check if the timer has exceeded the interval
        if (timer >= interval)
        {
            // Reset the timer
            timer = 0f;

            // Attempt to move all the conveyors
            foreach (ConveyorBelt conveyor in conveyors)
            {
                conveyor.ResetMove();
            }
            foreach (ConveyorBelt conveyor in conveyors)
            {
                conveyor.AttemptMove();
            }
        }
    }
}
