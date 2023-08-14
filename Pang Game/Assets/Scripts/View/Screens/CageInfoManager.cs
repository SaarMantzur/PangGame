using TMPro;
using UnityEngine;

/// <summary>
/// this class represents all visual information of the game 
/// </summary>
public class CageInfoManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelNumberText;

    public void SetData(int levelNumber)
    {
        _levelNumberText.text = levelNumber.ToString();
    }
}
