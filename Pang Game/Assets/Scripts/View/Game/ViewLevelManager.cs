using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewLevelManager : MonoBehaviour
{

    [SerializeField] private BallView _originalBallView;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private RawImage _background;

    [SerializeField] private ProjectileMovement _originalProjectileResource;
    [SerializeField] private Transform _projectileStartPoint;

    private ProjectileMovement _projectileMovement;

    private List<BallView> _createdBallView = new List<BallView>();

    private bool _isProjectileActive = false;
    private void Awake()
    {
        _canvas.renderMode = RenderMode.ScreenSpaceCamera;
        _canvas.worldCamera = Camera.main;

        //Used to avoid balls from coliding with each other.
        //The layer of _originalBallView is defualtly set to 3 => GameData.BallsLayer.
        int ballsLayer = GameData.BallsLayer;
        Physics2D.IgnoreLayerCollision(ballsLayer, ballsLayer);

        EventsManager.SplitEvent.AddListener(Split);
        EventsManager.FinishLevelEvent.AddListener(ClearLevel);

        EventsManager.ProjectileDestroyedEvent.AddListener(() => { _isProjectileActive = false; });
        EventsManager.FireEvent.AddListener(InitializeProjectile);

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
        if(levelInstructions.BackgroundImage != null)
            _background.texture = levelInstructions.BackgroundImage;
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

    private void InitializeProjectile()
    {
        if (!_isProjectileActive)
        {
            _isProjectileActive = true;
            _projectileMovement = Instantiate(_originalProjectileResource);
            _projectileMovement.transform.position = _projectileStartPoint.position;
        }
    }

    public void ClearLevel()
    {
        if (_createdBallView.Count > 0)
        {
            foreach (BallView ballView in _createdBallView)
            {
                Destroy(ballView.gameObject);
            }
        }

        if(_isProjectileActive)
        {
            _projectileMovement.DestroyProjectile();
        }

        _createdBallView.Clear();
        _player.transform.localPosition = Vector2.zero;
    }
}
