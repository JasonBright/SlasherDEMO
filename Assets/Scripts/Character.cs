using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Character : MonoBehaviour, IHitable
{
    [SerializeField] private float baseMoveSpeed;

    [Header("Технические")] 
    [SerializeField] private Animator animator;
    [SerializeField] private Transform rightHandContainer;
    [SerializeField] private Transform leftHandContainer;
    
    public float BaseMoveSpeed => baseMoveSpeed;
    public float AdditionalMoveSpeed { get; set; }

    public IUseable CurrentWeapon { get; private set; }

    public BuffsContainer BuffsContainer { get; private set; }

    private GameObject currentWeaponInstance;

    [SerializeField] private WeaponBuffProp weapon;

    void Awake()
    {
        BuffsContainer = new BuffsContainer();
        BuffsContainer.AddBuffDependency<Character>(this);
        
        BuffsContainer.AddBuff(weapon.Create());
    }

    void Update()
    {
        if (CurrentWeapon != null)
        {
            CurrentWeapon.Use(new UseParameters()
            {
                ActiveBuffs = null,
                Direction = transform.forward
            });
        }
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
}