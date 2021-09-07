using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Countdown : ITickeable
{
    public event Action Elapsed;
    public bool IsElapsed => timeLeft.HasValue == false || timeLeft <= 0;

    private float? timeLeft;
    
    public void Tick(float deltaTime)
    {
        timeLeft -= deltaTime;
        if (timeLeft <= 0)
        {
            Stop();
        }
    }

    public void AddTime(float time)
    {
        if (timeLeft.HasValue == false)
        {
            Start(time);
        }
        else
        {
            timeLeft += time;
        }
    }

    public void Start(float time)
    {
        if (timeLeft.HasValue)
        {
            throw new Exception("Timer already started");
        }
        timeLeft = time;
    }

    public void Stop()
    {
        timeLeft = null;
        Elapsed?.Invoke();
    }
}