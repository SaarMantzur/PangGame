using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CageInfoManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelNumberText;

    public void SetData(int levelNumber)
    {
        _levelNumberText.text = levelNumber.ToString();
    }
}
