using UnityEngine;
using UnityEngine.UI;

public class GameViewManager : MonoBehaviour
{
    //The Canvas of the scene
    [SerializeField] private Canvas _gameCanvas;

    //The prefab of the game menu GameObject
    [SerializeField] private GameObject _gameMenu;

    //The Level view parts
    [SerializeField] private ViewLevelManager _viewLevelManager;

    [SerializeField] private FinishLevelScreen _finishLevelScreen;

    [SerializeField] private GameOverScreenManager _gameOverScreenManager;

    //The Core Game manager
    private CoreGameFlow _coreGameFlow;

    private GameMenuManager _gameMenuManager;

    private bool _isLevelInstantiated = false;

    private bool _isLevelScreenInstantiated = false;

    private void Awake()
    {
        EventsManager.StartNewLevelEvent.AddListener(InitializeGameLevel);
        EventsManager.FinishLevelEvent.AddListener(InitializeFinishLevelScreen);
        EventsManager.ShowGameMenuEvent.AddListener(OnShowMenuEvent);
        EventsManager.StartGameOnDefaultLevelEvent.AddListener(()=>EventsManager.StartGameEvent.Invoke(_coreGameFlow.GetLevelNumber()));
        InitializeGameMenu();
        _coreGameFlow = new CoreGameFlow();
    }

    private void OnShowMenuEvent()
    {
        _finishLevelScreen.gameObject.SetActive(false);
        _gameMenuManager.SetCurrentLevel(_coreGameFlow.GetLevelNumber());
    }

    private void InitializeFinishLevelScreen()
    {
        //Make sure the _finishLevelScreen is only instantiated once to the scene.
        if (!_isLevelScreenInstantiated)
        {
            _finishLevelScreen = Instantiate(_finishLevelScreen);
            _isLevelScreenInstantiated = true;
            _finishLevelScreen.gameObject.SetActive(true);
            _finishLevelScreen.SetData(_coreGameFlow.GetLevelNumber());

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

    private void InitializeGameLevel(DataStructures.LevelInstructions levelInstructions)
    {
        //Make sure the ViewLevelManager is only instantiated once to the scene.
        if(!_isLevelInstantiated)
        {
            _viewLevelManager = Instantiate(_viewLevelManager);
            _isLevelInstantiated = true;
        }
        _viewLevelManager.CreateLevelByLevelData(levelInstructions);

        if(_finishLevelScreen.gameObject.activeSelf)
        {
            _finishLevelScreen.gameObject.SetActive(false);
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
            InitializeGameMenuByRectTransform(menuRectTransform);
            _gameMenuManager = _gameMenu.GetComponent<GameMenuManager>();
        }
    }

    /// <summary>
    /// Initializes the game menu ny RectTransform componnent.
    /// </summary>
    /// <param name="rectTransform">The RectTransform Componnent of the menu</param>
    private void InitializeGameMenuByRectTransform(RectTransform rectTransform)
    {
        rectTransform.SetParent(_gameCanvas.transform);
        rectTransform.offsetMax = new Vector2(0, 0);
        rectTransform.offsetMin = new Vector2(0, 0);
    }


}
