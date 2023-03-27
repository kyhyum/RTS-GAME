using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private AIUnit units;

    public IdleState(AIUnit aIUnit)
    {
        units = aIUnit;
    }

    public void Enter()
    {
        units.States = AIUnit.State.Walk;
    }

    public void Exit()
    {
        units.unit.StartMethod();
    }

    public void Stay()
    {
        throw new System.NotImplementedException();
    }

}
