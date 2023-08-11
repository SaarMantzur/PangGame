using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallView : MonoBehaviour
{

    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private Rigidbody2D _rigidbody2;

    [SerializeField] private float _velocityScalar = 0.2f;

    private Color _color;

    public int _size;

    private int _direction;

    private void Update()
    {
        _rigidbody2.AddForce(new Vector2((float)_direction, 0) * _velocityScalar);
    }

    public Color GetColor()
    {
        return _color;
    }

    public int GetSize()
    {
        return _size;
    }

    public int GetDirection()
    {
        return _direction;
    }
    public void SetDirection(int dir)
    {
        _direction = dir;
    }

    public void SetColor(Color color)
    {
        if(_spriteRenderer != null)
        {
            _spriteRenderer.color = color;
            _color = color;
        }
    }

    public void SetSize(int sizeNumber)
    {
        if(sizeNumber <= GameData.MaxBallSize && sizeNumber >= GameData.MinBallSize)
        {
            _spriteRenderer.transform.localScale *= Mathf.Pow(2, sizeNumber);
            _size = sizeNumber;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        int collisionLayerNumber = collision.gameObject.layer;
        switch (collisionLayerNumber)
        {
            //ball hit one of the walls
            case 6:
                //change direction of the ball
                SetDirection(_direction *= (-1));
                break;

            //ball hit Player
            case 7:
                //kill player
                EventsManager.EndGameEvent.Invoke();
                break;

            //Player shot ball
            case 8:
                //split ball
                EventsManager.SplitEvent.Invoke(this);
                break;
        }
    }


    private void OnMouseDown()
    {
        EventsManager.SplitEvent.Invoke(this);   
    }
}
