using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    [Header("Fields")]
    [SerializeField] Image image;
    [SerializeField] TMP_Text text;

    [Header("Values")]
    [SerializeField] public ItemType type;

    [Header("Timer")]
    public bool hasTimer = false;
    public int time = 30;
    public Slider slider;

    private float timer = 0f; // The timer that counts up to the interval


    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        image.sprite = type.render;
        text.text = type.name;

        if (hasTimer)
        {
            Timer();
        }
        
    }

    public void Timer()
    {
        // Increment the timer
        timer += Time.deltaTime;

        slider.value = timer / time;

        // Check if the timer has exceeded the interval
        if (timer >= time)
        {
            // Time is up!
        }
    }
}
