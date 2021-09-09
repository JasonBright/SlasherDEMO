using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Buffs/Create DebuffAbillityBuff")]
public class DebuffAbillityProp : BuffProp
{
    [Range(0, 100)]
    [SerializeField] private int activateChance;
    [SerializeField] private BuffProp buff;

    public int ActivateChance => activateChance;
    public BuffProp Buff => buff;

    public override Buff Create()
    {
        return new DebuffAbillity(this);
    }
}