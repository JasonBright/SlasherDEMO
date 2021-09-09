using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance { get; private set; }
    
    void Awake()
    {
        Instance = this;
    }

    public bool IsFriendly(Transform sender, IHitable target)
    {
        return false;
    }
}
