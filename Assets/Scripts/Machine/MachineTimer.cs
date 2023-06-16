using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MachineTimer : MonoBehaviour
{
    public float interval = 1f; // The interval in seconds between each update
    private float timer = 0f; // The timer that counts up to the interval

    [HideInInspector] public UnityEvent timerInterval;

    private void Awake()
    {
        if (timerInterval == null)
            timerInterval = new UnityEvent();
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

            Interval();
        }
    }

    private void Interval()
    {
        timerInterval.Invoke();
        //GetComponent<Machine>().Interval();
    }
}
