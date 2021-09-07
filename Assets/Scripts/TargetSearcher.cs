using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSearcher : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    
    public IHitable Search(Vector3 direction)
    {
        return new Dummy();
        
        if (Physics.Raycast(transform.position, direction, out var hitInfo, maxDistance: maxDistance) //рейкаст
            && hitInfo.collider.TryGetComponent<IHitable>(out var target)                            // это можно атаковать?
            && GameRoot.Instance.IsFriendly(transform, target) == false)                                   // это враг?
        {
            return target;
        }
        return null;
    }
}

public class Dummy : IHitable
{
    
}
