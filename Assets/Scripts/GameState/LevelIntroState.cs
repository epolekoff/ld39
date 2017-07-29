using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelIntroState : AbsState {

    public override void Update(IStateMachineEntity entity)
    {
        base.Update(entity);

        entity.GetStateMachine().ChangeState(new MainGameState());
    }
}
