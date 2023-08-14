using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonsInputData : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Button _button;

    private UnityAction _onButtonPressedAction;
    private UnityAction _onButtonUnPressedAction;
    private bool _isPressed;

    private void Awake()
    {
        EventsManager.EndGameEvent.AddListener(() => _isPressed = false);
        EventsManager.FinishLevelEvent.AddListener(() => _isPressed = false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
        _onButtonUnPressedAction.Invoke();
    }

    public void SetOnButtonPressedAction(UnityAction unityAction)
    {
        _onButtonPressedAction = unityAction;
    }

    public void SetOnButtonUnPressedAction(UnityAction unityAction)
    {
        _onButtonUnPressedAction = unityAction;
    }
    private void Update()
    {
        if (_isPressed)
        {
            _onButtonPressedAction?.Invoke();
        }
    }
}
