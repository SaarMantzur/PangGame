using UnityEngine;

/// <summary>
/// This class is the controller of the entire game.
/// It uses the _coreGameFlow as a Model and all the other ViewObjects as view.
/// No other class holds it as its instance, 
/// so all other view objects an model object communicate with it using events. 
/// </summary>
public class GameContorllerManager : MonoBehaviour
{
    //The Canvas of the scene
    [SerializeField] private Canvas _gameCanvas;

    [SerializeField] private GameObject _gameMenu;

    [SerializeField] private AudioManager _audioManager;

    [SerializeField] private ViewLevelManager _viewLevelManager;

    [SerializeField] private FinishLevelScreen _finishLevelScreen;

    [SerializeField] private GameOverScreenManager _gameOverScreenManager;

    [SerializeField] private FinishGameScreenManager _finishGameScreenManager;


    private CoreGameFlow _coreGameFlow;

    private GameMenuManager _gameMenuManager;

    //these bolean fields are meant to check wether the 
    private bool _isLevelInstantiated = false;
    private bool _isLevelScreenInstantiated = false;
    private bool _isGameOverScreenInstatiated = false;
    private bool _isGameFinishedScreenInstatiated = false;

    private void Awake()
    {
        _coreGameFlow = new CoreGameFlow();

        //Initialize all events
        EventsManager.StartGameEvent.AddListener(OnInitializeNewLevel);
        EventsManager.FinishLevelEvent.AddListener(OnFinishLevel);
        EventsManager.ShowGameMenuEvent.AddListener(OnShowMenuEvent);
        EventsManager.EndGameEvent.AddListener(OnGameOver);
        EventsManager.RestartAllLevels.AddListener(OnRestartAllLevelsAndStartGame);

        InitializeGameMenu();
        InitializeAudioSource();
        _viewLevelManager.SetData(_coreGameFlow.GetLevelNumber());
    }

    /// <summary>
    /// called when new level is initialize
    /// </summary>
    private void OnInitializeNewLevel()
    {
        InitializeGameLevel(_coreGameFlow.StartNewLevel());
    }

    //Restarts all levels to zero and starts the game
    private void OnRestartAllLevelsAndStartGame()
    {
        _coreGameFlow.ResetLevelsToZero();
        OnInitializeNewLevel();
        _gameMenuManager.HideShowMenuEvent(false);
        _viewLevelManager.SetData(_coreGameFlow.GetLevelNumber());
    }

    //Initialize the AudioSource at the begining of the play
    private void InitializeAudioSource()
    {
        _audioManager = Instantiate(_audioManager);
    }

    #region screen initializers
    private void OnGameOver()
    {
        InitializeGameOverScreen();
        _viewLevelManager.ClearLevel();
    }

    /// <summary>
    /// Show the menu Screen
    /// </summary>
    private void OnShowMenuEvent()
    {
        _finishGameScreenManager.gameObject.SetActive(false);
        _finishLevelScreen.gameObject.SetActive(false);
        _gameOverScreenManager.gameObject.SetActive(false);
        _gameMenuManager.SetCurrentLevel(_coreGameFlow.GetLevelNumber());
        _gameMenuManager.HideShowMenuEvent(true);
    }

    /// <summary>
    /// Initialize the game over screen
    /// </summary>
    private void InitializeGameOverScreen()
    {
        //Make sure the _finishLevelScreen is only instantiated once to the scene.
        if (!_isGameOverScreenInstatiated)
        {
            _gameOverScreenManager = Instantiate(_gameOverScreenManager);
            _isGameOverScreenInstatiated = true;
            _gameOverScreenManager.gameObject.SetActive(true);

            RectTransform rectTransform = _gameOverScreenManager.GetComponent<RectTransform>();

            rectTransform.SetParent(_gameCanvas.transform);
            rectTransform.offsetMax = new Vector2(0, 0);
            rectTransform.offsetMin = new Vector2(0, 0);

        }
        else
        {
            _gameOverScreenManager.gameObject.SetActive(true);
        }
        _gameOverScreenManager.SetData(_coreGameFlow.GetLevelNumber());
    }

    /// <summary>
    /// Initialize the screen of the finish game
    /// </summary>
    private void InitializeFinishGameScreen()
    {
        if (!_isGameFinishedScreenInstatiated)
        {
            _finishGameScreenManager = Instantiate(_finishGameScreenManager);
            _isGameFinishedScreenInstatiated = true;
            _finishGameScreenManager.gameObject.SetActive(true);

            RectTransform rectTransform = _finishGameScreenManager.GetComponent<RectTransform>();

            rectTransform.SetParent(_gameCanvas.transform);
            rectTransform.offsetMax = new Vector2(0, 0);
            rectTransform.offsetMin = new Vector2(0, 0);

        }
        else
        {
            _finishGameScreenManager.gameObject.SetActive(true);
        }
    }

    private void OnFinishLevel()
    {
        if(_coreGameFlow.FinishLevel())
        {
            InitializeFinishLevelScreen();
            _viewLevelManager.SetData(_coreGameFlow.GetLevelNumber());
        }
        else
        {
            InitializeFinishGameScreen();
        }
    }

    /// <summary>
    /// Initializr the screen of finish level
    /// </summary>
    private void InitializeFinishLevelScreen()
    {
        //Make sure the _finishLevelScreen is only instantiated once to the scene.
        if (!_isLevelScreenInstantiated)
        {
            _finishLevelScreen = Instantiate(_finishLevelScreen);
            _isLevelScreenInstantiated = true;
            _finishLevelScreen.gameObject.SetActive(true);

            RectTransform rectTransform = _finishLevelScreen.GetComponent<RectTransform>();

            rectTransform.SetParent(_gameCanvas.transform);
            rectTransform.offsetMax = new Vector2(0, 0);
            rectTransform.offsetMin = new Vector2(0, 0);

        }
        else
        {
            _finishLevelScreen.gameObject.SetActive(true);
        }
        _finishLevelScreen.SetData(_coreGameFlow.GetLevelNumber());
    }

    /// <summary>
    /// Initialize all items in the game based on the data stored in levelInstruction
    /// </summary>
    private void InitializeGameLevel(DataStructures.LevelInstructions levelInstructions)
    {
        if (levelInstructions != null)
        {
            //Make sure the ViewLevelManager is only instantiated once to the scene.
            if (!_isLevelInstantiated)
            {
                _viewLevelManager = Instantiate(_viewLevelManager);
                _isLevelInstantiated = true;
            }
            _viewLevelManager.CreateLevelByLevelData(levelInstructions);

            if (_finishLevelScreen.gameObject.activeSelf)
            {
                _finishLevelScreen.gameObject.SetActive(false);
            }


            if (_gameOverScreenManager.gameObject.activeSelf)
            {
                _gameOverScreenManager.gameObject.SetActive(false);
            }

            if(_gameMenuManager != null)
            {
                _gameMenuManager.HideShowMenuEvent(false);
            }
        }
    }

    /// <summary>
    /// Choosing to initialize the menu this way (instead of defualtly leave 
    /// the menu in the scene) will allow more dynamic changes in the future.
    /// It is enough to just change the prefab "GameMenu" instead of changing the actual scene.
    /// </summary>
    private void InitializeGameMenu()
    {
        if (_gameMenu != null && _gameCanvas != null)
        {
            _gameMenu = Instantiate(_gameMenu);
            RectTransform menuRectTransform = _gameMenu.GetComponent<RectTransform>();
            InitializeScreenRectTransform(menuRectTransform);
            _gameMenuManager = _gameMenu.GetComponent<GameMenuManager>();
            _gameMenuManager.SetCurrentLevel(_coreGameFlow.GetLevelNumber());
        }
    }

    /// <summary>
    /// Initializes the game menu ny RectTransform componnent.
    /// </summary>
    /// <param name="rectTransform">The RectTransform Componnent of the menu</param>
    private void InitializeScreenRectTransform(RectTransform rectTransform)
    {
        rectTransform.SetParent(_gameCanvas.transform);
        rectTransform.offsetMax = new Vector2(0, 0);
        rectTransform.offsetMin = new Vector2(0, 0);
    }
    #endregion
}
