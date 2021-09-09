using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityDependencyInjection;
using UnityEngine;

public class BuffsContainer
{
    public IList<Buff> ActiveBuffs => activeBuffs.AsReadOnly();
    
    private readonly List<Buff> activeBuffs = new List<Buff>();

    private readonly  Dictionary<Buff, ITickeable> timers = new Dictionary<Buff, ITickeable>();

    private IContainer container;

    public BuffsContainer(IContainer container)
    {
        this.container = container;
    }

    public void AddBuff(Buff buff)
    {
        container.InjectTo(buff);
        activeBuffs.Add(buff);
        if (buff.Prop.IsTimeLimited)
        {
            var timer = new Countdown();
            timer.Elapsed += () => BuffTimerElapsed(buff);
            timer.deb = "guf ";
            timer.Start(buff.Prop.Duration);
            timers.Add( buff, timer );
        }
        
        buff.Execute();
    }

    public void RemoveBuff(Buff buff)
    {
        if (activeBuffs.Contains(buff) == false)
        {
            throw new Exception("Buff isn't active in this container");
        }
        
        buff.Stop();
        activeBuffs.Remove(buff);
        if (timers.ContainsKey(buff))
        {
            var timer = timers[ buff ];
            timer.Stop(true);
            timers.Remove(buff);
        }
    }

    private void BuffTimerElapsed(Buff buff)
    {
        RemoveBuff(buff);
    }

    public void UpdateTimers(float deltaTime)
    {
        var tickables = timers.Select(x => x.Value).ToList();
        for(int i = tickables.Count - 1; i >= 0; i--)
        {
            tickables[i].Tick(deltaTime);
        }
    }
}