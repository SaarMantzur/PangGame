using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is the controller of the Player. 
/// It controlls its movement tghrough the rigidbody
/// and the animation by its Animator
/// </summary>
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Animator MovementAnimator;
    [SerializeField] private Rigidbody2D Rigidbody2D;
    [SerializeField] private SpeereMovement _speereMovement;

    [SerializeField] private float velocity = 2;

    private bool _isSpeereActive = false;

    private readonly string _moveRightCommand = "MoveRight";
    private readonly string _moveLeftCommand = "MoveLeft";
    private readonly string _fireCommand = "Fire";

    private void Awake()
    {
        EventsManager.MoveLeftEvent.AddListener(MoveLeft);
        EventsManager.MoveRightEvent.AddListener(MoveRight);
        EventsManager.FireEvent.AddListener(Fire);
        EventsManager.MoveIdleEvent.AddListener(CommitIdle);
        EventsManager.SpeereDestroyed.AddListener(() => { _isSpeereActive = false; });

        //avoid the speere hit the player
        Physics2D.IgnoreLayerCollision(9, 8);

        //avoid the speere hit the floor
        Physics2D.IgnoreLayerCollision(10, 8);
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
        if (!_isSpeereActive)
        {
            Rigidbody2D.velocity = new Vector2(0, 0);
            MovementAnimator.SetBool(_fireCommand, true);
            InitializeSpeere();
        }
    }

    private void InitializeSpeere()
    {
        _isSpeereActive = true;
        SpeereMovement speereMovement = Instantiate(_speereMovement);
        speereMovement.transform.position = transform.position - Vector3.up;
        
    }
}
