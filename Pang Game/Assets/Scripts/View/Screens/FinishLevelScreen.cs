using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class controls the Finish Level Screen 
/// </summary>
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
            //make the next level button invoke the start game event when pressed
            _nextLevelButton.onClick.AddListener(EventsManager.StartGameEvent.Invoke);
        }

        if (_backToMenuButton != null)
        {
            //make the back to menu button invoke the show game menu event when pressed
            _backToMenuButton.onClick.AddListener(EventsManager.ShowGameMenuEvent.Invoke);
        }
    }

    public void SetData(int currentLevelNumber)
    {
        _currentLevelNumber = currentLevelNumber;
        _nextLevelText.text = "Ready For Level " + _currentLevelNumber + "?";
    }
}
