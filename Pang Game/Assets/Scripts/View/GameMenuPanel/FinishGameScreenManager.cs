using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the finish game screen UI elements and interactions.
/// </summary>
public class FinishGameScreenManager : MonoBehaviour
{
    [SerializeField] private Button _goToMenuButton;
    [SerializeField] private Button _exitGameButton;

    /// <summary>
    /// Initializes UI button listeners during the Awake phase.
    /// </summary>
    private void Awake()
    {
        //Set up the "Go to Menu" button listener
        _goToMenuButton.onClick.AddListener(EventsManager.ShowGameMenuEvent.Invoke);

        //Set up the "Exit Game" button listener
        _exitGameButton.onClick.AddListener(Application.Quit);
    }
}