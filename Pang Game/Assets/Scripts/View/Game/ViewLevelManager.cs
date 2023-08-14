using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DataStructures;

/// <summary>
/// Manages the visual aspects and behavior of a game level's view.
/// </summary>
public class ViewLevelManager : MonoBehaviour
{
    #region serielized fields
    [SerializeField] private BallView _originalBallView;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private RawImage _background;
    [SerializeField] private TimeSlider _timeSlider;
    [SerializeField] private ProjectileMovement _originalProjectileResource;
    [SerializeField] private Transform _projectileStartPoint;
    [SerializeField] private CageInfoManager _cageManagerInfo;
    [SerializeField] private Button _backToMenuButton;

    #endregion

    private ProjectileMovement _projectileMovement;
    private int _levelNumber;

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
        EventsManager.BallHitRoofEvent.AddListener(DestroyBall);

        _backToMenuButton.onClick.AddListener(()=> { EventsManager.ShowGameMenuEvent.Invoke();ClearLevel();});

        //call the player movement by events recived from the UI
        EventsManager.MoveLeftEvent.AddListener(_player.MoveLeft);
        EventsManager.MoveRightEvent.AddListener(_player.MoveRight);
        EventsManager.FireEvent.AddListener(_player.Fire);
        EventsManager.MoveIdleEvent.AddListener(_player.CommitIdle);

        
    }


    private void Start()
    {
        _cageManagerInfo.SetData(_levelNumber);
    }

    public void SetData(int levelNumber)
    {
        _levelNumber = levelNumber;
    }

    /// <summary>
    /// Split a ball into 2 balls
    /// </summary>
    /// <param name="ballView"></param>
    private void Split(BallView ballView)
    {
        if (ballView.GetSize() > 1)
        {
            BallData leftBallData = new BallData();
            leftBallData.BallColor = ballView.GetColor();
            leftBallData.BallDirection = -1;
            leftBallData.BallSize = ballView.GetSize() - 1;
            leftBallData.BallLocation = ballView.transform.localPosition;
            CreateNewBall(leftBallData, true);

            BallData rightBallData = new BallData();
            rightBallData.BallColor = ballView.GetColor();
            rightBallData.BallDirection = 1;
            rightBallData.BallSize = ballView.GetSize() - 1;
            rightBallData.BallLocation = ballView.transform.localPosition;
            CreateNewBall(rightBallData, true);
        }

        DestroyBall(ballView);

        if (_createdBallView.Count == 0)
            EventsManager.FinishLevelEvent.Invoke();


    }

    /// <summary>
    /// removes ball from list and delete its GameObject
    /// </summary>
    /// <param name="ballView"></param>
    private void DestroyBall(BallView ballView)
    {
        if(ballView != null)
        {
            _createdBallView.Remove(ballView);
            Destroy(ballView.gameObject);
        }
    }

    /// <summary>
    /// Create the level by the data from LevelInstructions
    /// </summary>
    /// <param name="levelInstructions"></param>
    public void CreateLevelByLevelData(LevelInstructions levelInstructions)
    {
        foreach (var ballData in levelInstructions.BallsData)
        {
            CreateNewBall(ballData, false);
        }
        if(levelInstructions.BackgroundImage != null)
            _background.texture = levelInstructions.BackgroundImage;
        _timeSlider.SetTime(levelInstructions.TimeLengthInSeconds);
        _timeSlider.StartTime();
    }


    /// <summary>
    /// Create a new ball based on the data from ball data
    /// </summary>
    /// <param name="ballData">the data used to describe the ball</param>
    /// <param name="isSplitted">true if the it is required to create a new ball beacuase of a split</param>
    private void CreateNewBall(BallData ballData, bool isSplitted)
    {
        BallView ballView = Instantiate(_originalBallView);
        if (ballView != null)
        {
            ballView.SetDirection(ballData.BallDirection);
            ballView.SetColor(ballData.BallColor);
            ballView.SetSize(ballData.BallSize);
            ballView.transform.position = ballData.BallLocation;

            //In case the ball was created because of a split,
            //it is usefull if the ball would "jump" a little 
            //bit up and not just fall to the ground
            if(isSplitted)
            {
                StartCoroutine(ballView.Jump());
            }
            _createdBallView.Add(ballView);
        }
    }

    /// <summary>
    /// Initialize a new projectile object
    /// </summary>
    private void InitializeProjectile()
    {
        if (!_isProjectileActive)
        {
            _isProjectileActive = true;
            _projectileMovement = Instantiate(_originalProjectileResource);
            _projectileMovement.transform.position = _projectileStartPoint.position;
            EventsManager.ProjectileSentEvent.Invoke();
        }
    }

    /// <summary>
    /// Removes all elements from the game and returns player to its original position
    /// </summary>
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
        _player.CommitIdle();
        _player.RestoreToOriginalPosition();
        _cageManagerInfo.SetData(_levelNumber);
        _timeSlider.ResetTime();
    }
}
