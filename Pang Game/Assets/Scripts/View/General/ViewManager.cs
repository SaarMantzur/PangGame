using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewManager : MonoBehaviour
{
    //The Canvas of the scene
    [SerializeField] private Canvas _gameCanvas;

    [SerializeField] private GameObject _gameMenu;

    [SerializeField] private AudioManager _audioManager;

    [SerializeField] private ViewLevelManager _viewLevelManager;

    [SerializeField] private FinishLevelScreen _finishLevelScreen;

    [SerializeField] private GameOverScreenManager _gameOverScreenManager;

    [SerializeField] private FinishGameScreenManager _finishGameScreenManager;

    private GameMenuManager _gameMenuManager;

    //these bolean fields are meant to check wether the 
    private bool _isLevelInstantiated = false;
    private bool _isLevelScreenInstantiated = false;
    private bool _isGameOverScreenInstatiated = false;
    private bool _isGameFinishedScreenInstatiated = false;


    private int _levelNumber;
    private void Awake()
    {
        InitializeGameMenu();
        InitializeAudioSource();
    }

    public void SetData(int levelNumer)
    {
        _viewLevelManager.SetData(levelNumer);
    }

    public void HideShowMenu(bool showMenu)
    {
        _gameMenuManager.HideShowMenuEvent(showMenu);
    }

    //Initialize the AudioSource at the begining of the play
    private void InitializeAudioSource()
    {
        _audioManager = Instantiate(_audioManager);
    }
    #region screen initializers
    public void OnGameOver()
    {
        InitializeGameOverScreen();
        _viewLevelManager.ClearLevel();
    }

    /// <summary>
    /// Show the menu Screen
    /// </summary>
    public void OnShowMenuEvent()
    {
        _finishGameScreenManager.gameObject.SetActive(false);
        _finishLevelScreen.gameObject.SetActive(false);
        _gameOverScreenManager.gameObject.SetActive(false);
        _gameMenuManager.SetCurrentLevel(_levelNumber);
        _gameMenuManager.HideShowMenuEvent(true);
    }

    /// <summary>
    /// Initialize the game over screen
    /// </summary>
    public void InitializeGameOverScreen()
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
        _gameOverScreenManager.SetData(_levelNumber);
    }

    /// <summary>
    /// Initialize the screen of the finish game
    /// </summary>
    public void InitializeFinishGameScreen()
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



    /// <summary>
    /// Initializr the screen of finish level
    /// </summary>
    public void InitializeFinishLevelScreen()
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
        _finishLevelScreen.SetData(_levelNumber);
    }

    /// <summary>
    /// Initialize all items in the game based on the data stored in levelInstruction
    /// </summary>
    public void InitializeGameLevel(DataStructures.LevelInstructions levelInstructions)
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

            if (_gameMenuManager != null)
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
    public void InitializeGameMenu()
    {
        if (_gameMenu != null && _gameCanvas != null)
        {
            _gameMenu = Instantiate(_gameMenu);
            RectTransform menuRectTransform = _gameMenu.GetComponent<RectTransform>();
            InitializeScreenRectTransform(menuRectTransform);
            _gameMenuManager = _gameMenu.GetComponent<GameMenuManager>();
            _gameMenuManager.SetCurrentLevel(_levelNumber);
        }
    }

    /// <summary>
    /// Initializes the game menu ny RectTransform componnent.
    /// </summary>
    /// <param name="rectTransform">The RectTransform Componnent of the menu</param>
    public void InitializeScreenRectTransform(RectTransform rectTransform)
    {
        rectTransform.SetParent(_gameCanvas.transform);
        rectTransform.offsetMax = new Vector2(0, 0);
        rectTransform.offsetMin = new Vector2(0, 0);
    }
    #endregion

}
