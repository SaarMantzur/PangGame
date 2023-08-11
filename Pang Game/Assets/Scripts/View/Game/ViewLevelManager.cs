using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewLevelManager : MonoBehaviour
{

    [SerializeField] private BallView _originalBallView;
    [SerializeField] private Canvas _canvas;

    private List<BallView> _createdBallView = new List<BallView>();

    private void Awake()
    {
        _canvas.renderMode = RenderMode.ScreenSpaceCamera;
        _canvas.worldCamera = Camera.main;
    }

    public void CreateLevelByNumber(int levelNumber)
    {
        //Ask for Level Data by number
        DataStructures.BallData ballData = new DataStructures.BallData();
        ballData.BallColor = Color.blue;
        ballData.BallLocation = new Vector2(3, 3);
        ballData.BallSize = 6;

        DataStructures.BallData ballData1 = new DataStructures.BallData();
        ballData1.BallColor = Color.red;
        ballData1.BallLocation = new Vector2(-1, 3);
        ballData1.BallSize = 5;

        DataStructures.BallData ballData2 = new DataStructures.BallData();
        ballData2.BallColor = Color.red;
        ballData2.BallLocation = new Vector2(-4, 3);
        ballData2.BallSize = 4;

        DataStructures.LevelInstructions levelInstructions = new DataStructures.LevelInstructions();

        levelInstructions.ballsData.Add(ballData);
        levelInstructions.ballsData.Add(ballData1);
        levelInstructions.ballsData.Add(ballData2);


        CreateLevelByLevelData(levelInstructions);
    }

    private void CreateLevelByLevelData(DataStructures.LevelInstructions levelInstructions)
    {
        foreach (var ballData in levelInstructions.ballsData)
        {
            BallView ballView = Instantiate(_originalBallView);
            if(ballView != null)
            {
                ballView.SetColor(ballData.BallColor);
                ballView.SetSize(ballData.BallSize);
                ballView.transform.localPosition = ballData.BallLocation;
            }
        }
    }
}
