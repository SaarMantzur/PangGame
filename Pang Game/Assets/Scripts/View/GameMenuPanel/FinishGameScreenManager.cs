using UnityEngine;
using UnityEngine.UI;

public class FinishGameScreenManager : MonoBehaviour
{
    [SerializeField] private Button _goToMenuButton;
    [SerializeField] private Button _exitGameButton;

    private void Awake()
    {
        _goToMenuButton.onClick.AddListener(EventsManager.ShowGameMenuEvent.Invoke);
        _exitGameButton.onClick.AddListener(Application.Quit);
    }
}