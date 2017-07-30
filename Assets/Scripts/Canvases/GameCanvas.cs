using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameCanvas : MonoBehaviour {

    public Text TimerText;

    public GameObject TimerPanel;

    public Slider PowerMeter;
    public Text PowerMeterText;

    public GameObject GameUI;

    public Button NextLevelButton;
    public Button MainMenuButton;
    public Button RetryButton;

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

    public void GoToNextLevel()
    {
        GameController.Instance.GoToNextLevel();
    }

    public void ShowGameFinishedAnimation()
    {
        TimerPanel.GetComponent<Animator>().SetBool("GameFinished", true);
    }

    public void EnableNextLevelButton(bool isNextLevel)
    {  
        if(isNextLevel)
        {
            NextLevelButton.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(NextLevelButton.gameObject);
        }
        else
        {
            MainMenuButton.gameObject.SetActive(true);
            EventSystem.current.SetSelectedGameObject(MainMenuButton.gameObject);
        }
    }

    public void EnableRetryButton()
    {
        RetryButton.gameObject.SetActive(true);
        EventSystem.current.SetSelectedGameObject(RetryButton.gameObject);
    }
}
