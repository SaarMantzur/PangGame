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
        Debug.Log("Saar: " + PlayerPrefs.GetInt(_savedLevelPrefes));

        GameFlowLevelsList.Add(CreateLevel());
        GameFlowLevelsList.Add(CreateLevel1());
        //GameFlowLevelsList.Add(CreateLevel2());
        //GameFlowLevelsList.Add(CreateLevel3());
        //GameFlowLevelsList.Add(CreateLevel4());

        EventsManager.StartGameEvent.AddListener((i) => 
        {
            if (i<GameFlowLevelsList.Count)
                EventsManager.StartNewLevelEvent.Invoke(GameFlowLevelsList[i]);
        });
    }

    public bool FinishLevel()
    {
        if (_levelNumber < GameFlowLevelsList.Count - 1)
        {
            _levelNumber++;
            PlayerPrefs.SetInt(_savedLevelPrefes, _levelNumber);
            return true;
        }
        else
        {
            EventsManager.FinishGameEvent.Invoke();
            PlayerPrefs.SetInt(_savedLevelPrefes, 0);
            _levelNumber = 0;
            return false;
        }
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
        BallData ballData1 = new BallData(1, new Vector2(-1, 2), Color.red, 0);

        BallData ballData2 = new BallData(1, new Vector2(-2, 2), Color.cyan, 0);

        LevelInstructions levelInstructions = new LevelInstructions();

        levelInstructions.ballsData.Add(ballData1);
        levelInstructions.ballsData.Add(ballData2);

        return levelInstructions;
    }

    private LevelInstructions CreateLevel2()
    {
        BallData ballData1 = new BallData(2, new Vector2(-1, 2),Color.red, 1);

        BallData ballData2 = new BallData(1, new Vector2(-4, 2), Color.green, -1);

        LevelInstructions levelInstructions = new LevelInstructions();

        levelInstructions.ballsData.Add(ballData1);
        levelInstructions.ballsData.Add(ballData2);

        return levelInstructions;
    }

    private LevelInstructions CreateLevel3()
    {
        BallData ballData = new BallData(3,new Vector2(3,3), Color.blue, 2);

        LevelInstructions levelInstructions = new LevelInstructions();
        levelInstructions.ballsData.Add(ballData);

        return levelInstructions;
    }

    private LevelInstructions CreateLevel4()
    {
        BallData ballData = new BallData(4, new Vector2(3, 3), Color.magenta, 2);

        LevelInstructions levelInstructions = new LevelInstructions();
        levelInstructions.ballsData.Add(ballData);

        return levelInstructions;
    }

}
