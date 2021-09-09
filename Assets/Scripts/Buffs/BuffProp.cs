using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffProp : ScriptableObject
{
    [SerializeField] private float duration;

    public bool IsTimeLimited => duration > 0;
    public float Duration => duration;

    public virtual Buff Create()
    {
        throw new NotImplementedException();
    }
}