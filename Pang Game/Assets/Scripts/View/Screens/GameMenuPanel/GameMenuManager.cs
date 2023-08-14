using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


/// <summary>
/// This class represents the menu abilities
/// </summary>
public class GameMenuManager : MonoBehaviour
{
    #region Menu Items
    //The StartGame Button which will be responsible for starting the game
    [SerializeField] private Button _startGameButton;

    //Quit Game button will be responsible for quiting the game.
    [SerializeField] private Button _quitGameButton;

    //Restart levels Buttton
    [SerializeField] private Button _restartLevelButton;

    //The prefab of the game menu GameObject
    [SerializeField] private Image _backgroundImage;

    //The prefab of the game menu GameObject
    [SerializeField] private GameObject _buttonsPanel;
    #endregion

    //the current level number
    private int _currentLevel;

    //check if GameMenu is visible or not, to avoid unnecassery calls.
    private bool IsVisible = true;

    private void Awake()
    {
        //Attach Quit button to its purpose.
        _quitGameButton.onClick.AddListener(Application.Quit);

        //Attach StartGame button to its purpose.
        _startGameButton.onClick.AddListener(() => EventsManager.StartGameEvent.Invoke());

        //Attach _restartLevelButton to its purpose.
        _restartLevelButton.onClick.AddListener(EventsManager.RestartAllLevels.Invoke);
    }

    public void SetCurrentLevel(int currentLevel)
    {
        _currentLevel = currentLevel;
    }

    /// <summary>
    /// Disables or enabels the GameMenu
    /// </summary>
    public void HideShowMenuEvent(bool show)
    {
        if (IsVisible != show)
        {
            if (_buttonsPanel != null && _backgroundImage != null)
            {
                _buttonsPanel.SetActive(show);
                _backgroundImage.enabled = show;
                IsVisible = show;
            }
            else
            {
                print("Either ButtonsPanel or BackgroundImage is null! Cannot " + (show ? "show" : "hide") + " 'MenuPanel'");
            }
        }
    }
}
