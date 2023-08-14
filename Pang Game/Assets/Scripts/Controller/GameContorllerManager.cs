using UnityEngine;

/// <summary>
/// This class is the controller of the entire game.
/// It uses the _coreGameFlow as a Model and all the other ViewObjects as view.
/// No other class holds it as its instance, 
/// so all other view objects an model object communicate with it using events. 
/// </summary>
public class GameContorllerManager : MonoBehaviour
{

    private CoreGameFlow _coreGameFlow;
    [SerializeField] private ViewManager _viewManager;
    
    private void Awake()
    {
        _coreGameFlow = new CoreGameFlow();

        //Initialize all events
        EventsManager.StartGameEvent.AddListener(OnInitializeNewLevel);
        EventsManager.FinishLevelEvent.AddListener(OnFinishLevel);
        EventsManager.ShowGameMenuEvent.AddListener(_viewManager.OnShowMenuEvent);
        EventsManager.EndGameEvent.AddListener(_viewManager.OnGameOver);
        EventsManager.RestartAllLevels.AddListener(OnRestartAllLevelsAndStartGame);


        _viewManager.SetData(_coreGameFlow.GetLevelNumber());
    }

    /// <summary>
    /// called when new level is initialize
    /// </summary>
    private void OnInitializeNewLevel()
    {
        _viewManager.InitializeGameLevel(_coreGameFlow.StartNewLevel());
    }

    //Restarts all levels to zero and starts the game
    private void OnRestartAllLevelsAndStartGame()
    {
        _coreGameFlow.ResetLevelsToZero();
        OnInitializeNewLevel();
        _viewManager.HideShowMenu(false);
        _viewManager.SetData(_coreGameFlow.GetLevelNumber());
    }

    private void OnFinishLevel()
    {
        if (_coreGameFlow.FinishLevel())
        {
            _viewManager.InitializeFinishLevelScreen();
            _viewManager.SetData(_coreGameFlow.GetLevelNumber());
        }
        else
        {
            _viewManager.InitializeFinishGameScreen();
        }
    }


}
