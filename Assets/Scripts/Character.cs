using System.Collections;
using System.Collections.Generic;
using UnityDependencyInjection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Character : MonoBehaviour, IHitable, IContainer, IAliveable
{
    [SerializeField] private float baseMoveSpeed;
    [SerializeField] private float angularSpeed;
    [SerializeField] private float maxHealth;
    
    [Header("Технические")] 
    [SerializeField] private Animator animator;
    [SerializeField] private Transform rightHandContainer;
    [SerializeField] private Transform leftHandContainer;
    [SerializeField] private CharacterController characterController;
    
    public float BaseMoveSpeed => baseMoveSpeed;
    public float AdditionalMoveSpeed { get; set; }

    public IUseable CurrentWeapon { get; private set; }

    public BuffsContainer BuffsContainer { get; private set; }
    
    public float Health { get; private set; }
    public float MaxHealth => maxHealth;
    public bool IsAlive { get; private set; }

    public bool IsFreezing { get; set; } = false;

    private GameObject currentWeaponInstance;
    private readonly DependencyContainer dependencyContainer = new DependencyContainer();

    [SerializeField] private WeaponBuffProp weapon;
    [SerializeField] private BuffProp stunAbl;

    void Awake()
    {
        AddDependency<Character>(this);
        
        BuffsContainer = new BuffsContainer(this);
        AddDependency<BuffsContainer>(BuffsContainer);
        AddDependency<Animator>(animator);

        BuffsContainer.AddBuff(weapon.Create());
        if (stunAbl != null)
        {
            BuffsContainer.AddBuff(stunAbl.Create());
        }

        Health = MaxHealth;
    }

    void Update()
    {
        if (CurrentWeapon != null && !IsFreezing)
        {
            CurrentWeapon.Use(new UseParameters()
            {
                ActiveBuffs = BuffsContainer.ActiveBuffs,
                Direction = transform.forward
            });
        }
        
        BuffsContainer.UpdateTimers(Time.deltaTime);
    }

    public void Move(Vector2 input)
    {
        if (IsFreezing)
            return;
        
        var isInputed = input.sqrMagnitude != 0;

        if (isInputed)
        {
            transform.Rotate(Vector3.up, input.x * angularSpeed );
            characterController.SimpleMove(transform.forward * (input.y * GetMoveSpeed()));
        }
        
        animator.SetBool("Walk", isInputed);
    }

    public float GetMoveSpeed()
    {
        return BaseMoveSpeed + AdditionalMoveSpeed;
    }

    public void SetWeapon(GameObject prefab, Vector3 localPosition, Vector3 localRotation, CharacterSlot slot)
    {
        var prefabInstance = Instantiate( prefab, GetWeaponParent(slot) );
        prefabInstance.transform.localPosition = localPosition;
        prefabInstance.transform.localEulerAngles = localRotation;
        
        if (prefabInstance.TryGetComponent<IUseable>(out var weapon))
        {
            weapon.Animator = animator;
            CurrentWeapon = weapon;
            
            if (currentWeaponInstance != null)
            {
                Destroy(currentWeaponInstance);
            }
            currentWeaponInstance = prefabInstance;
        }
        else
        {
            Debug.LogWarning($"This weapon prefab {prefab.name} have no IUseable realization");
            Destroy(prefabInstance);
        }
    }

    private Transform GetWeaponParent(CharacterSlot slot)
    {
        Transform parent = rightHandContainer;
        switch (slot)
        {
            case CharacterSlot.LeftHand:
                parent = leftHandContainer;
                break;
            case CharacterSlot.RightHand:
                parent = rightHandContainer;
                break;
        }
        return parent;
    }

    public void InjectTo(object target)
    {
        dependencyContainer.InjectTo(target);
    }

    public void AddDependency<T>(object dependency) where T : class
    {
        dependencyContainer.Add(dependency as T);
    }

    public void Hit(params Impacter[] impacts)
    {
        foreach (var impact in impacts)
        {
            InjectTo(impact);
            impact.Execute(this);
        }
    }
    
    public void Damage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            IsAlive = false;
        }
    }
}