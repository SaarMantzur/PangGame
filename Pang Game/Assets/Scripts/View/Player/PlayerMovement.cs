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
    private readonly int _projectileLayer = 8;
    private readonly int _playerLayer = 9;
    private readonly int _floorLayer = 10;

    [SerializeField] private Animator _movementAnimator;
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [SerializeField] private float _velocity = 2;

    private bool _isProjectileActive = false;

    private readonly string _moveRightCommand = "MoveRight";
    private readonly string _moveLeftCommand = "MoveLeft";
    private readonly string _fireCommand = "Fire";

    private void Awake()
    {
        EventsManager.MoveLeftEvent.AddListener(MoveLeft);
        EventsManager.MoveRightEvent.AddListener(MoveRight);
        EventsManager.FireEvent.AddListener(Fire);
        EventsManager.MoveIdleEvent.AddListener(CommitIdle);

        //avoid the Projectile hit the player
        Physics2D.IgnoreLayerCollision(_playerLayer, _projectileLayer);

        //avoid the Projectile hit the floor
        Physics2D.IgnoreLayerCollision(_floorLayer, _projectileLayer);
    }

    public void CommitIdle()
    {
        _rigidbody2D.velocity = new Vector2(0, 0);
        _movementAnimator.SetBool(_moveRightCommand, false);
        _movementAnimator.SetBool(_moveLeftCommand, false);
        _movementAnimator.SetBool(_fireCommand, false);
    }

    public void MoveRight()
    {
        _rigidbody2D.velocity = new Vector2(_velocity, 0);
        _movementAnimator.SetBool(_moveRightCommand, true);
    }

    public void MoveLeft()
    {
        _rigidbody2D.velocity = new Vector2(-_velocity, 0);
        _movementAnimator.SetBool(_moveLeftCommand, true);
    }

    public void Fire()
    {
        if (!_isProjectileActive)
        {
            _rigidbody2D.velocity = new Vector2(0, 0);
            _movementAnimator.SetBool(_fireCommand, true);
        }
    }
}
