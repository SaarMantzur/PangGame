using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructures
{

    public class LevelInstructions
    {
        public List<BallData> BallsData = new List<BallData>();
        public Texture BackgroundImage;
        public float TimeLengthInSeconds;
    }

    public class BallData
    {
        //The number representing the ball size
        public int BallSize;

        //Ball Distance From Left Buttom Corner
        public Vector2 BallLocation;

        //The Color in which the ball shall apear.
        public Color BallColor;

        //The directopn in which the ball shall move first. (-1 for left and 1 for right. Any other number would increase the speed to that direction.)
        public int BallDirection;


        public BallData(int ballSize, Vector2 ballLocation, Color ballColor, int ballDirection)
        {
            BallSize = ballSize;
            BallLocation = ballLocation;
            BallColor = ballColor;
            BallDirection = ballDirection;
        }

        public BallData()
        {

        }
    }

}
