using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour, IHitable
{
    [SerializeField] private float baseMoveSpeed;

    [Header("Технические")] 
    [SerializeField] private Animator animator;
    [SerializeField] private Transform weaponContainer;

    public float BaseMoveSpeed => baseMoveSpeed;
    public float AdditionalMoveSpeed { get; set; }

    public IUseable CurrentWeapon { get; private set; }

    public BuffsContainer BuffsContainer { get; private set; }

    private GameObject currentWeaponInstance;

    void Awake()
    {
        BuffsContainer = new BuffsContainer();
        BuffsContainer.AddBuffDependency<Character>(this);
    }

    public void SetWeapon(GameObject prefab, Vector3 localPosition, Vector3 localRotation)
    {
        var prefabInstance = Instantiate(prefab, weaponContainer);
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
}