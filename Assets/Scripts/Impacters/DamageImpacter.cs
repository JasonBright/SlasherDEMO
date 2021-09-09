using System.Collections;
using System.Collections.Generic;
using UnityDependencyInjection;
using UnityEngine;

public class DamageImpacter : Impacter
{
    [Inject] private IAliveable target;
    [Inject] private Animator targetAnimator;

    public override void Execute(IHitable target)
    {
        this.target.Damage( 5 );
        targetAnimator.SetTrigger("Impact");
    }
}
