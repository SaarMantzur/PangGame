using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A script for managing a time-based slider that represents a progress bar.
/// </summary>
public class TimeSlider : MonoBehaviour
{
    //Reference to the Slider UI element for displaying the progress.
    [SerializeField] private Slider _progressBarSlider;

    //The maximum time value for the progress.
    private float _maxTime;

    //Flag to indicate if the time is currently being counted.
    private bool _isCountingTime = false;

    //The elapsed time since the counting started.
    float _elapsedTime = 0;

    /// <summary>
    /// If the time is being counted, update the progress of the slider.
    /// </summary>
    private void Update()
    {
        if (_isCountingTime)
        {

            if (_maxTime > _elapsedTime)
            {
                //Calculate the progress value between 0 and 1 based on elapsed time.
                float progress = _elapsedTime / _maxTime;
                _progressBarSlider.value = progress;
            }
            else
            {
                EventsManager.EndGameEvent.Invoke();
                PauseTime();
            }

            //update the elapsed time.
            _elapsedTime += Time.deltaTime;

        }
    }

    /// <summary>
    /// Pause the time counting.
    /// </summary>
    private void PauseTime()
    {
        _isCountingTime = false;
    }

    /// <summary>
    /// Start counting time from 0 and update the progress bar.
    /// </summary>
    public void StartTime()
    {
        _isCountingTime = true;
        _progressBarSlider.value = 0;
    }

    /// <summary>
    /// Reset the elapsed time and pause the time counting.
    /// </summary>
    public void ResetTime()
    {
        _elapsedTime = 0;
        _isCountingTime = false;
    }

    /// <summary>
    /// Set the maximum time value for the progress bar.
    /// </summary>
    /// <param name="timeInSeconds">The maximum time value in seconds.</param>

    public void SetTime(float timeInSeconds)
    {
        _maxTime = timeInSeconds;
    }
}
