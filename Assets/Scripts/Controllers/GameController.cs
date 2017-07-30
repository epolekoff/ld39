using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>, IStateMachineEntity {

    private FiniteStateMachine m_stateMachine;
    public FiniteStateMachine GetStateMachine(int number = 0)
    {
        return m_stateMachine;
    }

    public Transform PlayerSpawnPoint;

    public int CurrentLevel;
    private const int NumLevels = 4;

    public float CurrentGameTime { get; set; }
    public float CurrentPowerLevel { get; set; }

    public bool GameStarted = false;
    public bool GameFinished = false;

    public Player Player { get; set; }

    // Use this for initialization
    void Awake () {
        // Initialize variables
        CurrentPowerLevel = 1f;

        // Create the player
        Player = PlayerFactory.CreatePlayer();

        m_stateMachine = new FiniteStateMachine(new LevelIntroState(), this);

    }

    // Update is called once per frame
    void Update () {
        m_stateMachine.Update();

    }

    /// <summary>
    /// Use some charge after an attack.
    /// </summary>
    public void UseCharge(float amount)
    {
        CurrentPowerLevel -= amount;
        if(CurrentPowerLevel < 0)
        {
            CurrentPowerLevel = 0;
        }
    }

    /// <summary>
    /// Add a charge to the current charge. If the player was already at max, return false.
    /// </summary>
    public bool AddCharge(float amount)
    {
        if (CurrentPowerLevel >= 1)
        {
            return false;
        }
        else
        {
            CurrentPowerLevel += amount;
            if (CurrentPowerLevel >= 1)
            {
                CurrentPowerLevel = 1;
            }
        }
        return true;
    }

    public void Victory()
    {
        GameFinished = true;
        m_stateMachine.ChangeState(new LevelVictoryState());
    }

    public void KillPlayer()
    {
        if (GameFinished)
            return;
        m_stateMachine.ChangeState(new LevelDeathState());
    }

    public void GoToNextLevel()
    {
        if(IsNextLevel())
        {
            SceneManager.LoadScene(0);
        }
        SceneManager.LoadScene(CurrentLevel + 1);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(CurrentLevel);
    }

    public bool IsNextLevel()
    {
        return CurrentLevel != NumLevels;
    }
}
