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



    // Use this for initialization
    void Start () {
        m_stateMachine = new FiniteStateMachine(new LevelIntroState(), this);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
