using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : MonoBehaviour, IUseable
{
    [SerializeField] private float attackCountdown;
    
    public Animator Animator { get; set; }
    
    private Countdown countdown = new Countdown();
    
    public bool Use(UseParameters parameters)
    {
        if (countdown.IsElapsed == false)
        {
            return false;
        }
        
        countdown.Start(attackCountdown);
        Animator.SetTrigger("Attack");
        return true;
    }
}
