using System;
using System.Collections;
using System.Collections.Generic;
using UnityDependencyInjection;
using UnityEngine;

public class BuffsContainer
{
    private readonly List<Buff> activeBuffs = new List<Buff>();

    private IContainer container;

    public BuffsContainer(IContainer container)
    {
        this.container = container;
    }

    public void AddBuff(Buff buff)
    {
        container.InjectTo(buff);
        activeBuffs.Add(buff);
        
        buff.Execute();
    }
}