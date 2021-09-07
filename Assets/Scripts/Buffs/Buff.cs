using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff
{
    public BuffProp Prop { get; private set; }
    
    public Buff(BuffProp prop)
    {
        Prop = prop;
    }

    public virtual void Execute()
    {
        
    }

    public virtual void Stop()
    {
        
    }
    
    public T GetProp<T>() where T : BuffProp
    {
        return Prop as T;
    }
}
