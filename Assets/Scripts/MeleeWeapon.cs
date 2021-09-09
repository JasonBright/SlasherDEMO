using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        Attack(target, parameters.ActiveBuffs.OfType<AttackModificatorBuff>().ToArray());
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

    public void Attack(IHitable target, AttackModificatorBuff[] attackModificatorBuffs)
    {
        var damageImpacter = new DamageImpacter();
        var impacters = attackModificatorBuffs.Select(x => x.GetImpacter()).Where(x => x != null).Concat( new []{damageImpacter}).ToArray();
        target.Hit(impacters);
    }
}