using UnityEngine;
using UnityEngine.UI;

public class GameViewManager : MonoBehaviour
{
    //The Canvas of the scene
    [SerializeField] private Canvas GameCanvas;

    //The prefab of the game menu GameObject
    [SerializeField] private GameObject GameMenu;
    


    private void Awake()
    {
        EventsManager.InitializeAllEvents();

        InitializeGameMenu();
    }


    /// <summary>
    /// Choosing to initialize the menu this way (instead of defualtly leave 
    /// the menu in the scene) will allow more dynamic changes in the future.
    /// It is enough to just change the prefab "GameMenu" instead of changing the actual scene.
    /// </summary>
    private void InitializeGameMenu()
    {
        if (GameMenu != null && GameCanvas != null)
        {
            GameMenu = Instantiate(GameMenu);
            RectTransform menuRectTransform = GameMenu.GetComponent<RectTransform>();
            InitializeGameMenuByRectTransform(menuRectTransform);
        }
    }


    /// <summary>
    /// Initializes the game menu ny RectTransform componnent.
    /// </summary>
    /// <param name="rectTransform">The RectTransform Componnent of the menu</param>
    private void InitializeGameMenuByRectTransform(RectTransform rectTransform)
    {
        rectTransform.SetParent(GameCanvas.transform);
        rectTransform.offsetMax = new Vector2(0, 0);
        rectTransform.offsetMin = new Vector2(0, 0);
    }


}
