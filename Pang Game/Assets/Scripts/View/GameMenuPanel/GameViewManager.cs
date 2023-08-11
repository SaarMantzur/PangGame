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

    private bool isLevelInstantiated = false;

    private void Awake()
    {
        InitializeGameMenu();
        EventsManager.StartGameEvent.AddListener(InitializeGameLevel);
    }

    private void InitializeGameLevel()
    {
        //Make sure the ViewLevelManager is only instantiated once to the scene.
        if(!isLevelInstantiated)
        {
            _viewLevelManager = Instantiate(_viewLevelManager);
            isLevelInstantiated = true;
        }
        _viewLevelManager.CreateLevelByNumber(1);

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
