using System;
using System.Collections;
using System.Collections.Generic;
using UnityDependencyInjection;
using UnityEngine;

public class BuffsContainer
{
    private readonly List<Buff> activeBuffs = new List<Buff>();
    private readonly DependencyContainer dependencyContainer = new DependencyContainer();
    
    public void AddBuff(Buff buff)
    {
        dependencyContainer.InjectTo(buff);
        activeBuffs.Add(buff);
        
        buff.Execute();
    }

    public void AddBuffDependency<T>(object dependency) where T : class
    {
        dependencyContainer.Add(dependency as T);
    }
}