using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataStructures;

public class CoreGameFlow : MonoBehaviour
{
    private List<LevelInstructions> GameFlowLevelsList = new List<LevelInstructions>();

    private void Awake()
    {
        GameFlowLevelsList.Add(CreateLevel1());
        GameFlowLevelsList.Add(CreateLevel2());

        EventsManager.StartGameEvent.AddListener((i) => 
        {
            print("Start GameEvent Called");
            EventsManager.StartNewLevelEvent.Invoke(GameFlowLevelsList[i]);
        });
    }

    private LevelInstructions CreateLevel1()
    {
        BallData ballData1 = new BallData();
        ballData1.BallColor = Color.red;
        ballData1.BallLocation = new Vector2(-1, 2);
        ballData1.BallSize = 5;
        ballData1.BallDirection = 1;

        BallData ballData2 = new BallData();
        ballData2.BallColor = Color.green;
        ballData2.BallLocation = new Vector2(-4, 2);
        ballData2.BallSize = 4;
        ballData2.BallDirection = -1;

        LevelInstructions levelInstructions = new LevelInstructions();

        levelInstructions.ballsData.Add(ballData1);
        levelInstructions.ballsData.Add(ballData2);

        return levelInstructions;
    }

    private LevelInstructions CreateLevel2()
    {
        BallData ballData = new BallData();
        ballData.BallColor = Color.blue;
        ballData.BallLocation = new Vector2(3, 3);
        ballData.BallSize = 6;
        ballData.BallDirection = 1;

        LevelInstructions levelInstructions = new LevelInstructions();
        levelInstructions.ballsData.Add(ballData);

        return levelInstructions;
    }

}
