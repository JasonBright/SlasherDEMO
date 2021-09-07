using System.Collections;
using System.Collections.Generic;
using UnityDependencyInjection;
using UnityEngine;

public class MoveSpeedBuff : Buff
{
    [Inject] private Character target;

    private float speedBonus;
    
    public MoveSpeedBuff(BuffProp prop) : base(prop)
    {
    }

    public override void Execute()
    {
        var bonusMultiplier = GetProp<MoveSpeedBuffProp>().SpeedBonusInPercents;
        speedBonus = target.BaseMoveSpeed * bonusMultiplier;
        target.AdditionalMoveSpeed += speedBonus;
        
        Debug.Log("T " + target.AdditionalMoveSpeed);
    }

    public override void Stop()
    {
        target.AdditionalMoveSpeed -= speedBonus;
    }
}