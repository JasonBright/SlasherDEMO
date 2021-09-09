using System.Collections;
using System.Collections.Generic;
using UnityDependencyInjection;
using UnityEngine;

public class StunnedBuff : Buff
{
    [Inject] private Character character;
    [Inject] private Animator animator;
    
    public StunnedBuff(BuffProp prop) : base(prop)
    {
    }

    public override void Execute()
    {
        character.IsFreezing = true;
        animator.SetBool("Dizzy", true);
    }

    public override void Stop()
    {
        character.IsFreezing = false;
        animator.SetBool("Dizzy", false);
    }
}
