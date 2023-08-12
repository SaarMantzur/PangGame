using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField] private Animator MovementAnimator;
    [SerializeField] private Rigidbody2D Rigidbody2D;

    private float velocity = 2;

    private readonly string _moveRightCommand = "MoveRight";
    private readonly string _moveLeftCommand = "MoveLeft";
    private readonly string _fireCommand = "Fire";


    private void Awake()
    {
        EventsManager.MoveLeftEvent.AddListener(MoveLeft);
        EventsManager.MoveRightEvent.AddListener(MoveRight);
        EventsManager.MoveIdleEvent.AddListener(Fire);
        EventsManager.MoveIdleEvent.AddListener(CommitIdle);
    }

    public void CommitIdle()
    {
        Rigidbody2D.velocity = new Vector2(0, 0);
        MovementAnimator.SetBool(_moveRightCommand, false);
        MovementAnimator.SetBool(_moveLeftCommand, false);
        MovementAnimator.SetBool(_fireCommand, false);
    }

    public void MoveRight()
    {
        Rigidbody2D.velocity = new Vector2(velocity, 0);
        MovementAnimator.SetBool(_moveRightCommand, true);
    }

    public void MoveLeft()
    {
        Rigidbody2D.velocity = new Vector2(-velocity, 0);
        MovementAnimator.SetBool(_moveLeftCommand, true);
    }

    public void Fire()
    {
        Rigidbody2D.velocity = new Vector2(-velocity, 0);
        MovementAnimator.SetBool(_fireCommand, true);
    }

   
}
