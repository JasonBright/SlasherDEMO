using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Buffs/Create MoveSpeedBuff")]
public class MoveSpeedBuffProp : BuffProp
{
    public float SpeedBonusInPercents;

    public override Buff Create()
    {
        return new MoveSpeedBuff(this);
    }
}
