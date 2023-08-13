using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeereMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2;
    [SerializeField] private BoxCollider2D _boxCollider2D;
    [SerializeField] private float _velocity;


    private Vector2 _originalPotition;
    private bool _isPullingOut = false;


    // Start is called before the first frame update
    void Start()
    {
        _originalPotition = transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody2.AddForce(new Vector2(0, _velocity));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int collisionLayer = collision.gameObject.layer;
        if(collisionLayer == 3 || collisionLayer == 7)
        {
                EventsManager.SpeereDestroyed.Invoke();
                Destroy(gameObject);
        }
    }
}
