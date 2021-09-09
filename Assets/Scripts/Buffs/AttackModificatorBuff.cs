using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackModificatorBuff : Buff
{
    protected AttackModificatorBuff(BuffProp prop) : base(prop)
    {
    }
    
    public abstract Impacter GetImpacter();

    
}
