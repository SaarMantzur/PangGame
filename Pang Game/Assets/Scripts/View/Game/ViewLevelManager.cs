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

        //Used to avoid balls from coliding with each other.
        //The layer of _originalBallView is defualtly set to 3 => GameData.BallsLayer.
        int ballsLayer = GameData.BallsLayer;
        Physics2D.IgnoreLayerCollision(ballsLayer, ballsLayer);

        EventsManager.SplitEvent.AddListener(Split);
    }

    private void Split(BallView ballView)
    {
        if (ballView.GetSize() > 1)
        {
            DataStructures.BallData leftBallData = new DataStructures.BallData();
            leftBallData.BallColor = ballView.GetColor();
            leftBallData.BallDirection = -1;
            leftBallData.BallSize = ballView.GetSize() - 1;
            leftBallData.BallLocation = ballView.transform.localPosition;
            CreateNewBall(leftBallData);

            DataStructures.BallData rightBallData = new DataStructures.BallData();
            rightBallData.BallColor = ballView.GetColor();
            rightBallData.BallDirection = 1;
            rightBallData.BallSize = ballView.GetSize() - 1;
            rightBallData.BallLocation = ballView.transform.localPosition;

            CreateNewBall(rightBallData);
        }

        _createdBallView.Remove(ballView);
        Destroy(ballView.gameObject);

        if(_createdBallView.Count == 0)
            EventsManager.FinishLevelEvent.Invoke();


    }

    public void CreateCurrentLevel()
    {
        //Ask for Level Data by number
        DataStructures.BallData ballData = new DataStructures.BallData();
        ballData.BallColor = Color.blue;
        ballData.BallLocation = new Vector2(3, 3);
        ballData.BallSize = 6;
        ballData.BallDirection = 1;

        DataStructures.BallData ballData1 = new DataStructures.BallData();
        ballData1.BallColor = Color.red;
        ballData1.BallLocation = new Vector2(-1, 2);
        ballData1.BallSize = 5;
        ballData1.BallDirection = 1;

        DataStructures.BallData ballData2 = new DataStructures.BallData();
        ballData2.BallColor = Color.green;
        ballData2.BallLocation = new Vector2(-4, 2);
        ballData2.BallSize = 4;
        ballData2.BallDirection = -1;

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
            CreateNewBall(ballData);
        }
    }

    private void CreateNewBall(DataStructures.BallData ballData)
    {
        BallView ballView = Instantiate(_originalBallView);
        if (ballView != null)
        {
            ballView.SetDirection(ballData.BallDirection);
            ballView.SetColor(ballData.BallColor);
            ballView.SetSize(ballData.BallSize);
            ballView.transform.localPosition = ballData.BallLocation;
            _createdBallView.Add(ballView);
        }
    }
}
