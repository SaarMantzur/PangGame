using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallView : MonoBehaviour
{

    [SerializeField] private SpriteRenderer spriteRenderer;


    public void SetColor(Color color)
    {
        if(spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }

    public void SetSize(int sizeNumber)
    {
        if(sizeNumber <= GameData.MaxBallSize && sizeNumber >= GameData.MinBallSize)
        {
            print("SizeNumber: " + sizeNumber);
            spriteRenderer.transform.localScale *= Mathf.Pow(2, sizeNumber);
            print(spriteRenderer.transform.localScale.ToString() + " *= " + Mathf.Pow(2, sizeNumber));
        }
    }

}
