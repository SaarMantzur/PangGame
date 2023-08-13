using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishLevelScreen : MonoBehaviour
{
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private Button _backToMenuButton;
    [SerializeField] private TextMeshProUGUI _nextLevelText;
    private int _currentLevelNumber;

    private void Awake()
    {
        if(_nextLevelButton != null)
        {
            _nextLevelButton.onClick.AddListener(()=>EventsManager.StartGameEvent.Invoke(_currentLevelNumber));
        }

        if (_backToMenuButton != null)
        {
            _backToMenuButton.onClick.AddListener(EventsManager.ShowGameMenuEvent.Invoke);
        }
    }

    public void SetData(int currentLevelNumber)
    {
        _currentLevelNumber = currentLevelNumber;
        _nextLevelText.text = "Ready For Level " + _currentLevelNumber + "?";
    }
}
