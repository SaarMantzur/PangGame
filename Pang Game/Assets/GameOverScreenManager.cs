using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenManager : MonoBehaviour
{
    [SerializeField] private Button _restartLevelButton;
    [SerializeField] private Button _backToMenuButton;

    private int _currentLevelNumber;

    private void Awake()
    {
        if (_restartLevelButton != null)
        {
            _restartLevelButton.onClick.AddListener(() => EventsManager.StartGameOnDefaultLevelEvent.Invoke());
        }

        if (_backToMenuButton != null)
        {
            _backToMenuButton.onClick.AddListener(EventsManager.ShowGameMenuEvent.Invoke);
        }
    }

    public void SetData(int currentLevelNumber)
    {
        _currentLevelNumber = currentLevelNumber;
    }

}
