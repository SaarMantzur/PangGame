using System.Collections.Generic;
using UnityEngine;
using static DataStructures;

/// <summary>
/// Represents the model layer of the Game.
/// In this class creating a new level is within the code,
/// without changing scenes.
/// 
/// The ultimate way to create a new level is with a server sending a Json \ Yaml files
/// to client, but because of time requirements and task requirements I have decided to use internal code
/// as an example of an easy way to edit the levels and add new ones.
/// </summary>
public class CoreGameFlow
{
    //stores the data of all the levels of the game
    private List<LevelInstructions> gameFlowLevelsList = new List<LevelInstructions>();
    private int _levelNumber;
    private string _savedLevelPrefes = "LevelNumber";

    public CoreGameFlow()
    {
        CreateAllLevels();
        _levelNumber = PlayerPrefs.GetInt(_savedLevelPrefes);
    }

    public LevelInstructions StartNewLevel()
    {
        if (_levelNumber < gameFlowLevelsList.Count)
            return gameFlowLevelsList[_levelNumber];
        return null;
    }

    /// <summary>
    /// Add all levels
    /// </summary>
    private void CreateAllLevels()
    {
        gameFlowLevelsList.Add(CreateLevel1());
        gameFlowLevelsList.Add(CreateLevel2());
        gameFlowLevelsList.Add(CreateLevel3());
        gameFlowLevelsList.Add(CreateLevel4());
        gameFlowLevelsList.Add(CreateLevel5());
    }

    /// <summary>
    /// called when a level is finishd to check the result
    /// </summary>
    /// <returns>returns true if moving to next level, false if all level are finished</returns>
    public bool FinishLevel()
    {
        if (_levelNumber < gameFlowLevelsList.Count - 1)
        {
            _levelNumber++;
            //save the result to device so will start the same level later
            PlayerPrefs.SetInt(_savedLevelPrefes, _levelNumber);
            return true;
        }
        else
        {
            EventsManager.FinishGameEvent.Invoke();
            ResetLevelsToZero();
            return false;
        }
    }

    public void ResetLevelsToZero()
    {
        PlayerPrefs.SetInt(_savedLevelPrefes, 0);
        _levelNumber = 0;
    }

    public int GetLevelNumber()
    {
        return _levelNumber;
    }

    #region level creations
    /// <summary>
    /// Inorder to create a level:
    /// </summary>
    /// <returns></returns>
    private LevelInstructions CreateLevel()
    {
        //a new ball is created
        //it has the minimum size of 1
        //has a position
        //has a color
        //has a moving direction( 0 - means it will only go up and down)
        //positive number will drive it right and negative number to the left
        BallData ballData1 = new BallData(1, new Vector2(-1, 2), Color.red, 0);


        LevelInstructions levelInstructions = new LevelInstructions();

        //add it to level instructions
        levelInstructions.BallsData.Add(ballData1);
        levelInstructions.TimeLengthInSeconds = 180;
        return levelInstructions;
    }

    private LevelInstructions CreateLevel1()
    {
        BallData ballData1 = new BallData(1, new Vector2(2, 2), Color.red, 1);

        BallData ballData2 = new BallData(1, new Vector2(-2, 2), Color.cyan, -1);

        LevelInstructions levelInstructions = new LevelInstructions();

        levelInstructions.BallsData.Add(ballData1);
        levelInstructions.BallsData.Add(ballData2);
        levelInstructions.TimeLengthInSeconds = 20;

        return levelInstructions;
    }

    private LevelInstructions CreateLevel2()
    {
        BallData ballData1 = new BallData(2, new Vector2(0, 2),Color.red, 1);

        BallData ballData2 = new BallData(1, new Vector2(-4, 2), Color.green, -1);

        LevelInstructions levelInstructions = new LevelInstructions();

        levelInstructions.BallsData.Add(ballData1);
        levelInstructions.BallsData.Add(ballData2);
        levelInstructions.TimeLengthInSeconds = 30;

        return levelInstructions;
    }

    private LevelInstructions CreateLevel3()
    {
        BallData ballData = new BallData(3,new Vector2(3,3), Color.blue, 2);

        LevelInstructions levelInstructions = new LevelInstructions();
        levelInstructions.BallsData.Add(ballData);
        levelInstructions.TimeLengthInSeconds = 60;

        return levelInstructions;
    }

    private LevelInstructions CreateLevel4()
    {
        BallData ballData = new BallData(4, new Vector2(3, 3), Color.magenta, 2);

        LevelInstructions levelInstructions = new LevelInstructions();
        levelInstructions.BallsData.Add(ballData);
        levelInstructions.TimeLengthInSeconds = 70;

        return levelInstructions;
    }

    private LevelInstructions CreateLevel5()
    {
        BallData ballData = new BallData(4, new Vector2(3, 3), Color.magenta, 1);
        BallData ballData1 = new BallData(4, new Vector2(0, 3), Color.magenta, -1);

        LevelInstructions levelInstructions = new LevelInstructions();
        levelInstructions.BallsData.Add(ballData);
        levelInstructions.BallsData.Add(ballData1);
        levelInstructions.TimeLengthInSeconds = 120;

        return levelInstructions;
    }
    #endregion
}
