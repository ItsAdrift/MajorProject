using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerRenderer : MonoBehaviour
{
    public GameTimer gameTimer;
    public TMP_Text text;

    // Update is called once per frame
    void Update()
    {
        text.text = Format(gameTimer.time);
    }

    private string Format(int time)
    {
        int m = time / 60;
        int s = time % 60;

        return m + ":" + (s < 10 ? ("0" + s) : (s == 0 ? "00" : s));
    }
}
