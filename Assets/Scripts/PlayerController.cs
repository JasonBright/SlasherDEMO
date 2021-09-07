using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Character Target;
    [SerializeField] private Transform playerPosition;
    private Controlls controlls;
    
    private void OnEnable()
    {
        controlls = new Controlls();
        controlls.Enable();
    }

    void Update()
    {
        Target.Move(controlls.Player.Move.ReadValue<Vector2>() );
        playerPosition.position = Target.transform.position;
    }
}
