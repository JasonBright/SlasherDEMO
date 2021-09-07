using System.Collections;
using System.Collections.Generic;
using UnityDependencyInjection;
using UnityEngine;

public class DamageImpacter : Impacter
{
    [Inject] private IAliveable target;
    
    public DamageImpacter(Impacter source) : base(source)
    {
    }

    protected override void Impact()
    {
        target.Damage( 5 );
    }
}
