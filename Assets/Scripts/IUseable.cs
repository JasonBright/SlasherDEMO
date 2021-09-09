using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUseable
{
    bool Use(UseParameters useParameters);
    public Animator Animator { get; set; }
    //bool CanUse(); //?
}

public struct UseParameters
{
    public IList<Buff> ActiveBuffs;
    public Vector3 Direction;
}
