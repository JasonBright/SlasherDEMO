using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IUseable
{
    [SerializeField] private float attackCountdown;

    [Header("Технические")] 
    [SerializeField] private AnimatorOverrideController animatorOverrideController;
    [SerializeField] private TargetSearcher targetSearcher;
    
    public Animator Animator { get; set; }
    
    private Countdown countdown = new Countdown();
    
    public bool Use(UseParameters parameters)
    {
        if (countdown.IsElapsed == false)
        {
            return false;
        }

        var target = targetSearcher.Search(parameters.Direction);
        if (target == null)
        {
            return false;
        }
        
        countdown.Start(attackCountdown);
        Animator.SetTrigger("Attack");
        return true;
    }

    void Start()
    {
        if (animatorOverrideController != null)
        {
            Animator.runtimeAnimatorController = animatorOverrideController;
        }
    }

    void Update()
    {
        countdown.Tick(Time.deltaTime);
    }
}