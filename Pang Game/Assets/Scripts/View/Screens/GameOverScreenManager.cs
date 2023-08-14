using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the game over screen UI elements and interactions.
/// </summary>
public class GameOverScreenManager : MonoBehaviour
{
    [SerializeField] private Button _restartLevelButton;
    [SerializeField] private Button _backToMenuButton;

    private int _currentLevelNumber;

    /// <summary>
    /// Initializes UI button listeners during the Awake phase.
    /// </summary>
    private void Awake()
    {
        // Set up the restart level button listener
        if (_restartLevelButton != null)
        {
            _restartLevelButton.onClick.AddListener(EventsManager.StartGameEvent.Invoke);
        }

        // Set up the back to menu button listener
        if (_backToMenuButton != null)
        {
            _backToMenuButton.onClick.AddListener(EventsManager.ShowGameMenuEvent.Invoke);
        }
    }

    /// <summary>
    /// Sets the current level number.
    /// </summary>
    /// <param name="currentLevelNumber">The current level number to set.</param>
    public void SetData(int currentLevelNumber)
    {
        _currentLevelNumber = currentLevelNumber;
    }

}
