using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLevelScreen : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _backToMenuButton;
    private int _currentLevelNumber;

    private void Awake()
    {
        if(_nextLevelButton != null)
        {
            _nextLevelButton.onClick.AddListener(()=> 
            {
                EventsManager.StartGameEvent.Invoke(_currentLevelNumber+1);
            });
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
