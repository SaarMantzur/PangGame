using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallView : MonoBehaviour
{

    //The sprite renderer of the ball
    [SerializeField] private SpriteRenderer _spriteRenderer;

    //the rigidbody for the physics of the ball
    [SerializeField] private Rigidbody2D _rigidbody2;

    //scalar to slow down the speed
    //[SerializeField] private float _moveSpeed = 0.001f;

    float _overBounceTime = 0.1f;
    float _accelaratorBounceForce = 20;

    //The color for the ball
    private Color _color;

    //size of the ball
    public int _size;

    //the direction the ball should face
    private int _direction;

    //in case its required to make the ball over bounce, 
    //its enough to change this value.
    public float _accelarator = 0;

    private void Update()
    {
        //move the ball on the x axis by the value of _direction, 
        //and on the y axis by the value of the _accelarator
        _rigidbody2.AddForce(new Vector2(_direction, _accelarator));// * _moveSpeed);
    }

    /// <summary>
    /// Used to make the ball bounce
    /// adds force to the accelerator for a specific period of time.
    /// </summary>
    /// <returns></returns>
    public IEnumerator Jump()
    {
        _accelarator = _accelaratorBounceForce;
        yield return new WaitForSeconds(_overBounceTime);
        _accelarator = 0;
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

            //ball hit the roof
            case 7:
                //destroy the ball
                EventsManager.SplitEvent.Invoke(this);
                break;

            //Player shot ball
            case 8:
                //split ball
                EventsManager.SplitEvent.Invoke(this);
                break;

            //ball hit Player
            case 9:
                //kill player
                EventsManager.EndGameEvent.Invoke();
                break;


        }
    }
}
