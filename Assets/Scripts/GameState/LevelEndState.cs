using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelVictoryState : AbsState
{
    public override void Enter(IStateMachineEntity entity)
    {
        base.Enter(entity);

        // Unhinge the camera from the racer.
        GameController.Instance.Player.GetComponent<PlayerBlast>().StopAllCoroutines();
        var camera = GameController.Instance.Player.GetComponentInChildren<Camera>();
        if (camera != null)
        {
            camera.transform.SetParent(null);
        }

        // Show an animation for the high score.
        UIController.Instance.GameCanvas.ShowGameFinishedAnimation();

        bool isNextLevel = GameController.Instance.IsNextLevel();
        UIController.Instance.GameCanvas.EnableNextLevelButton(isNextLevel);
    }
}

public class LevelDeathState : AbsState
{
    public override void Enter(IStateMachineEntity entity)
    {
        base.Enter(entity);

        // Unhinge the camera from the racer.
        GameController.Instance.Player.GetComponent<PlayerBlast>().StopAllCoroutines();
        var camera = GameController.Instance.Player.GetComponentInChildren<Camera>();
        if (camera != null)
        {
            camera.transform.SetParent(null);
        }

        // Show an animation for the high score.
        //UIController.Instance.GameCanvas.ShowGameFinishedAnimation();

        // Enable the retry button.
        UIController.Instance.GameCanvas.EnableRetryButton();
    }
}