using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructures
{

    public class LevelInstructions
    {
        public List<BallData> ballsData = new List<BallData>();
    }

    public class BallData
    {
        //The number representing the ball size (Only powers of 2)
        public int BallSize;

        //Ball Distance From Left Buttom Corner
        public Vector2 BallLocation;

        //The Color in which the ball shall apear.
        public Color BallColor;
    }

}
