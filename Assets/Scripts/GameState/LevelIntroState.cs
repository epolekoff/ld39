using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIntroState : AbsState {

    private float m_timer = 0f;

    private const float TransitionTime = 3f;

    public override void Enter(IStateMachineEntity entity)
    {
        //Disable the player movement when the intro is playing.
        GameController.Instance.Player.GetComponent<PlayerMovement>().MovementOverride = true;

        // Disable the blast ability
        GameController.Instance.Player.GetComponent<PlayerBlast>().MoveAllowed = false;
        GameController.Instance.GameStarted = false;

        UIController.Instance.GameCanvas.GameUI.SetActive(false);
    }

    public override void Update(IStateMachineEntity entity)
    {
        base.Update(entity);

        m_timer += Time.deltaTime;

        if(m_timer >= TransitionTime)
        {
            entity.GetStateMachine().ChangeState(new MainGameState());
        }
        
    }

    public override void Exit(IStateMachineEntity entity)
    {
        GameController.Instance.Player.GetComponent<PlayerMovement>().MovementOverride = false;
        GameController.Instance.Player.GetComponent<PlayerBlast>().MoveAllowed = true;

        GameController.Instance.GameStarted = true;
        UIController.Instance.GameCanvas.GameUI.SetActive(true);
    }

}
