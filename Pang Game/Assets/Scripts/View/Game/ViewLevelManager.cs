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

    public void CreateLevelByLevelData(DataStructures.LevelInstructions levelInstructions)
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
