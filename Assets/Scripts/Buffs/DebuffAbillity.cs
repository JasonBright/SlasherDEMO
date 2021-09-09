using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuffAbillity : AttackModificatorBuff
{
    public override Impacter GetImpacter()
    {
        var prop = GetProp<DebuffAbillityProp>();

        var rand = Random.Range(0, 101);
        if (rand <= prop.ActivateChance)
        {
            return new BuffImpacter(prop.Buff);
        }
        
        return null;
    }

    public DebuffAbillity(BuffProp prop) : base(prop)
    {
    }
}
