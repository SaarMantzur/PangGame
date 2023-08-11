using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructures
{

    public class LevelInstructions
    {
        public List<BallData> ballsData = new List<BallData>();
    }

    public class ItemData
    {

    }

    public class BallData : ItemData
    {
        //The number representing the ball size (Only powers of 2)
        public int BallSize;

        //Ball Distance From Left Buttom Corner
        public Vector2 BallLocation;

        //The Color in which the ball shall apear.
        public Color BallColor;

        //The directopn in which the ball shall move first. (-1 for left and 1 for right)
        public int BallDirection;
    }

}
