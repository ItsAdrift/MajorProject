using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public bool active = false;

    public int time = 120;

    public float interval = 1f; // The interval in seconds between each update
    private float timer = 0f; // The timer that counts up to the interval

    void Start()
    {
    }

    void Update()
    {
        if (!active)
            return;

        // Increment the timer
        timer += Time.deltaTime;

        // Check if the timer has exceeded the interval
        if (timer >= interval)
        {
            // Reset the timer
            timer = 0f;

            time--;

            if (time <= 0)
            {
                // Game Over
                GameManager.Instance.GameOver();
                enabled = false;
            }
        }
    }
}
