using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>, IStateMachineEntity {

    private FiniteStateMachine m_stateMachine;
    public FiniteStateMachine GetStateMachine(int number = 0)
    {
        return m_stateMachine;
    }

    public float CurrentGameTime { get; set; }
    public float CurrentPowerLevel { get; set; }

    // Use this for initialization
    void Start () {
        m_stateMachine = new FiniteStateMachine(new LevelIntroState(), this);

        // Initialize variables
        CurrentPowerLevel = 1f;
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
            Debug.Log("Overcharge");
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
}
