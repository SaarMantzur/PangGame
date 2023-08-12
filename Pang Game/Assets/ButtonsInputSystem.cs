using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonsInputSystem : MonoBehaviour
{
    [SerializeField] private ButtonsInputData _moveRightButton;
    [SerializeField] private ButtonsInputData _moveLeftButton;
    [SerializeField] private ButtonsInputData _fireButton;

    private void Awake()
    {
        _moveRightButton.SetOnButtonPressedAction(EventsManager.MoveRightEvent.Invoke);
        _moveRightButton.SetOnButtonUnPressedAction(EventsManager.MoveIdleEvent.Invoke);

        _moveLeftButton.SetOnButtonPressedAction(EventsManager.MoveLeftEvent.Invoke);
        _moveLeftButton.SetOnButtonUnPressedAction(EventsManager.MoveIdleEvent.Invoke);

        _fireButton.SetOnButtonPressedAction(EventsManager.FireEvent.Invoke);
        _fireButton.SetOnButtonUnPressedAction(EventsManager.MoveIdleEvent.Invoke);

    }
}
