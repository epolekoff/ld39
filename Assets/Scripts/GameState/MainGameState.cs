﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameState : AbsState {

    public override void Enter(IStateMachineEntity entity)
    {
        UIController.Instance.GameCanvas.GameUI.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    public override void Update(IStateMachineEntity entity)
    {
        base.Update(entity);

        // Keep the time updated, as long as we're in a game state.
        GameController.Instance.CurrentGameTime += Time.deltaTime;

        // Display the text on the screen.
        UIController.Instance.GameCanvas.DisplayTime(GameController.Instance.CurrentGameTime);

        // Update the power level
        UIController.Instance.GameCanvas.SetPowerMeterLevel(GameController.Instance.CurrentPowerLevel);

        // Cheats
        if(Input.GetKeyDown(KeyCode.P))
        {
            GameController.Instance.AddCharge(1);
        }
    }
}
