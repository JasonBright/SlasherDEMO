using System.Collections;
using System.Collections.Generic;
using UnityDependencyInjection;
using UnityEngine;

public class BuffImpacter : Impacter
{
    [Inject] private BuffsContainer target;
    
    private BuffProp buffProp;

    public BuffImpacter(BuffProp targetBuff)
    {
        buffProp = targetBuff;
    }

    public override void Execute(IHitable target)
    {
        this.target.AddBuff( buffProp.Create() );
    }
}
