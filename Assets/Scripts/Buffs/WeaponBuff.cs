using System.Collections;
using System.Collections.Generic;
using UnityDependencyInjection;
using UnityEngine;

public class WeaponBuff : Buff
{
    [Inject] private Character target;
    
    public WeaponBuff(BuffProp prop) : base(prop)
    {
    }

    public override void Execute()
    {
        var prop = GetProp<WeaponBuffProp>();
        target.SetWeapon(prop.Prefab, prop.LocalPosition, prop.LocalRotation);
    }
}
