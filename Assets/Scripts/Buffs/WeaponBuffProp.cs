using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data/Buffs/Create WeaponBuff")]
public class WeaponBuffProp : BuffProp
{
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] private Vector3 localPosition;
    [SerializeField] private Vector3 localRotation;
    [SerializeField] private CharacterSlot slot;

    public GameObject Prefab => weaponPrefab;
    public Vector3 LocalPosition => localPosition;
    public Vector3 LocalRotation => localRotation;
    public CharacterSlot Slot => slot;

    public override Buff Create()
    {
        return new WeaponBuff(this);
    }
}

public enum CharacterSlot
{
    LeftHand,
    RightHand,
}
