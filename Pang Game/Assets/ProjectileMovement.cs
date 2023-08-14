using UnityEngine;

/// <summary>
/// Manages the movement and behavior of a projectile object.
/// </summary>
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

    /// <summary>
    /// Handles collision events with other objects.
    /// </summary>
    /// <param name="collision">The collision information.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int collisionLayer = collision.gameObject.layer;

        // Destroy the projectile upon collision with certain layers
        if (collisionLayer == GameData.BallsLayer || collisionLayer == GameData.RoofLayer)
        {
            DestroyProjectile();
        }
    }

    /// <summary>
    /// Destroys the projectile and invokes the ProjectileDestroyedEvent.
    /// </summary>
    public void DestroyProjectile()
    {
        EventsManager.ProjectileDestroyedEvent.Invoke();
        Destroy(gameObject);
    }
}
