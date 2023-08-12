using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataStructures;

/// <summary>
/// Represents the model layer
/// </summary>
public class CoreGameFlow
{
    private List<LevelInstructions> GameFlowLevelsList = new List<LevelInstructions>();
    private int _levelNumber;
    private string _savedLevelPrefes = "LevelNumber";

    public CoreGameFlow()
    {
        GameFlowLevelsList.Add(CreateLevel());
        GameFlowLevelsList.Add(CreateLevel1());
        GameFlowLevelsList.Add(CreateLevel2());

        EventsManager.FinishLevelEvent.AddListener(FinishLevel);

        EventsManager.StartGameEvent.AddListener((i) => 
        {
            EventsManager.StartNewLevelEvent.Invoke(GameFlowLevelsList[i]);
        });
    }

    private void FinishLevel()
    {
        _levelNumber++;
        PlayerPrefs.SetInt(_savedLevelPrefes, _levelNumber);
    }

    public int GetLevelNumber()
    {
        return _levelNumber;
    }

    private LevelInstructions CreateLevel()
    {
        BallData ballData1 = new BallData(1, new Vector2(-1, 2), Color.red, 0);


        LevelInstructions levelInstructions = new LevelInstructions();

        levelInstructions.ballsData.Add(ballData1);

        return levelInstructions;
    }

    private LevelInstructions CreateLevel1()
    {
        BallData ballData1 = new BallData(5, new Vector2(-1, 2),Color.red, 1);

        BallData ballData2 = new BallData(4, new Vector2(-4, 2), Color.green, -1);

        LevelInstructions levelInstructions = new LevelInstructions();

        levelInstructions.ballsData.Add(ballData1);
        levelInstructions.ballsData.Add(ballData2);

        return levelInstructions;
    }

    private LevelInstructions CreateLevel2()
    {
        BallData ballData = new BallData(6,new Vector2(3,3), Color.blue, 1);

        LevelInstructions levelInstructions = new LevelInstructions();
        levelInstructions.ballsData.Add(ballData);

        return levelInstructions;
    }

}
