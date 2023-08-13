using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private float _velocity;

    // Update is called once per frame
    void Update()
    {
        _rigidbody2.velocity = Vector2.up * _velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int collisionLayer = collision.gameObject.layer;
        if(collisionLayer == 3 || collisionLayer == 7)
        {
            DestroyProjectile();
        }
    }

    public void DestroyProjectile()
    {
        EventsManager.ProjectileDestroyedEvent.Invoke();
        Destroy(gameObject);
    }
}
