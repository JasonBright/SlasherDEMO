using System;
using System.Timers;
using UnityEditor;

public interface ITickeable
{
    event Action Elapsed;
    bool IsElapsed { get; }
    void Tick(float deltaTime);
    void AddTime(float time);
    void Start(float time);
    void Stop(bool silentMode = false);
}