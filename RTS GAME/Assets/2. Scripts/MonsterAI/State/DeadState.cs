using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState
{
    private AIUnit Units;

    public DeadState(AIUnit aIUnit)
    {
        Units = aIUnit;
    }

    public void Enter()
    {
        Units.PlayAnimation(AIUnit.State.Walk);
    }

    public void Exit()
    {
        UnitSpawn.instance.Die(Units.gameObject, Units.character_num);
    }

    public void Stay()
    {
    }
}
