using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameMenuManager : MonoBehaviour
{

    //The StartGame Button which will be responsible for starting the game
    [SerializeField] private Button StartGameButton;

    //Quit Game button will be responsible for quiting the game.
    [SerializeField] private Button QuitGameButton;

    //The prefab of the game menu GameObject
    [SerializeField] private Image BackgroundImage;

    //The prefab of the game menu GameObject
    [SerializeField] private GameObject ButtonsPanel;


    //check if GameMenu is visible or not, to avoid unnecassery calls.
    private bool IsVisible = true;

    private void Awake()
    {
        //Attach Quit button to its purpose.
        QuitGameButton.onClick.AddListener(Application.Quit);

        //Attach StartGame button to its purpose.
        StartGameButton.onClick.AddListener(() =>
        {
            print("Button clicked");
            EventsManager.StartGameEvent.Invoke(0);
        });

        EventsManager.StartGameEvent.AddListener((i) => HideShowMenuEvent(false));
        EventsManager.ShowGameMenuEvent.AddListener(() => HideShowMenuEvent(true));
    }


    /// <summary>
    /// Disables or enabels the GameMenu
    /// </summary>
    private void HideShowMenuEvent(bool show)
    {
        if (IsVisible != show)
        {
            if (ButtonsPanel != null && BackgroundImage != null)
            {
                ButtonsPanel.SetActive(show);
                BackgroundImage.enabled = show;
                IsVisible = show;
            }
            else
            {
                print("Either ButtonsPanel or BackgroundImage is null! Cannot " + (show ? "show" : "hide") + " 'MenuPanel'");
            }
        }
    }
}
