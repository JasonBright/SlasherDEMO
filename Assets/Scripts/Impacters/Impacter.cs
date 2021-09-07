using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impacter
{
    private Impacter wrappee;
    
    public Impacter(Impacter source)
    {
        wrappee = source;
    }

    public void Execute(IHitable target)
    {
        Impact();
        wrappee?.Execute(target);
    }

    protected virtual void Impact()
    {
        new NotImplementedException();
    }
}
