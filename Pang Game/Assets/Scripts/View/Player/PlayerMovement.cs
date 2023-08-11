using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Animator MovementAnimator;
    [SerializeField] private Rigidbody2D Rigidbody2D;

    private readonly string _moveRightCommand = "MoveRight";
    private readonly string _moveLeftCommand = "MoveLeft";
    private readonly string _fireCommand = "Fire";


    private void Awake()
    {
        EventsManager.MoveLeftEvent.AddListener(MoveLeft);
        EventsManager.MoveRightEvent.AddListener(MoveRight);
    }

    private void CommitIdle()
    {
        Rigidbody2D.velocity = new Vector2(0, 0);
        MovementAnimator.SetBool(_moveRightCommand, false);
        MovementAnimator.SetBool(_moveLeftCommand, false);
        MovementAnimator.SetBool(_fireCommand, false);
    }

    private void MoveRight()
    {
        Rigidbody2D.velocity = new Vector2(1, 0);
        MovementAnimator.SetBool(_moveRightCommand, true);
    }

    private void MoveLeft()
    {
        Rigidbody2D.velocity = new Vector2(-1, 0);
        MovementAnimator.SetBool(_moveLeftCommand, true);
    }

    private void Fire()
    {
        Rigidbody2D.velocity = new Vector2(-1, 0);
        MovementAnimator.SetBool(_fireCommand, true);
    }

   
}
