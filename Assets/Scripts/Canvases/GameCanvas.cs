using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviour {

    public Text TimerText;

    public Slider PowerMeter;
    public Text PowerMeterText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Show the time on screen.
    /// </summary>
    /// <param name="value"></param>
    public void DisplayTime(float value)
    {
        TimerText.text = FormatTimeAsString(value);
    }

    /// <summary>
    /// Set the power meter.
    /// </summary>
    /// <param name="value"></param>
    public void SetPowerMeterLevel(float value)
    {
        PowerMeter.value = value;
        PowerMeterText.text = Mathf.Floor(value * 100).ToString() + "%";
    }

    /// <summary>
    /// Given a time value, format it in a readable way.
    /// </summary>
    private string FormatTimeAsString(float value)
    {
        int milliseconds = Mathf.FloorToInt((value - Mathf.Floor(value)) * 1000);
        var span = new TimeSpan(0, 0, 0, Mathf.FloorToInt(value), milliseconds);
        string timeFormat = string.Format("{0:00}:{1:00}:{2:000}", (int)span.TotalMinutes, span.Seconds, span.Milliseconds);
        return timeFormat;
    }
}
