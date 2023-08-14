using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class is used to determine the correct location
/// to set the colliders boundries of the scene in relation 
/// to the screen size (using the UI abilities)
/// </summary>
public class BoundsMatchToScreen : MonoBehaviour
{
    #region serielized fields
    [SerializeField] private BoxCollider2D _roofColliderTransform;
    [SerializeField] private BoxCollider2D _floorColliderTransform;
    [SerializeField] private BoxCollider2D _rightWallColliderTransform;
    [SerializeField] private BoxCollider2D _leftWallColliderTransform;

    [SerializeField] private RectTransform _roofUI;
    [SerializeField] private RectTransform _floorUI;
    [SerializeField] private RectTransform _rightWallUI;
    [SerializeField] private RectTransform _leftWallUI;
    #endregion

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
        MatchColliderToUI(_roofUI, _roofColliderTransform);
        MatchColliderToUI(_floorUI, _floorColliderTransform);
        MatchColliderToUI(_rightWallUI, _rightWallColliderTransform);
        MatchColliderToUI(_leftWallUI, _leftWallColliderTransform);
    }
    
    /// <summary>
    /// Using the rectTransfor coordinates in relations to camera 
    /// to determine the correct position for the collider and
    /// place it as needed.
    /// </summary>
    /// <param name="rectTransform"></param>
    /// <param name="boxCollider2D"></param>
    private void MatchColliderToUI(RectTransform rectTransform, BoxCollider2D boxCollider2D)
    {
        // Calculate the screen position of the UI element
        Vector2 uiElementScreenPosition = RectTransformUtility.WorldToScreenPoint(_mainCamera, rectTransform.position);

        // Convert the screen position back to world coordinates for the collider
        Vector3 colliderPosition = _mainCamera.ScreenToWorldPoint(uiElementScreenPosition);

        // Set the collider's position
        boxCollider2D.transform.position = colliderPosition;
    }
}
