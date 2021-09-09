using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Buffs/Create StunnedBuff")]
public class StunnedBuffProp : BuffProp
{
    [SerializeField] private float duration;

    public float Duration => duration;

    public override Buff Create()
    {
        return new StunnedBuff(this);
    }
}
