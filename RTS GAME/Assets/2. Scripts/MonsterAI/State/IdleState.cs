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
    }

    public void Exit()
    {
    }

    public void Stay()
    {
        if (!units.isDead && units.isCreep)
        {
            units.States = AIUnit.State.Walk;
        }
    }

}