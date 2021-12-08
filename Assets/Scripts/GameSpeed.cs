using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeed : MonoBehaviour
{
    private bool _timeOff = false;

    /// <summary>
    /// Toggles between pause and unpause
    /// </summary>
    public void Toggle()
    {
        if (_timeOff)
        {
            Unpause();
        }
        else
        {
            Pause();
        }
    }

    /// <summary>
    /// Sets the timescale to 0 so the game freezes
    /// </summary>
    private void Pause()
    {
        Time.timeScale = 0f;
        _timeOff = true;
    }

    /// <summary>
    /// Sets the timescale to 1 so the game runs at the normal speed
    /// </summary>
    private void Unpause()
    {
        Time.timeScale = 1f;
        _timeOff = false;
    }

    /// <summary>
    /// Speeds up the game's speed to the double of the normal 
    /// </summary>
    public void SpeedUp()
    {
        Time.timeScale = 2f;
    }

    public void Slow()
    {
        Time.timeScale = 0.5f;
    }
}
