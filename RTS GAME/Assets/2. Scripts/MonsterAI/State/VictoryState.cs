using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryState : IState
{
    private AIUnit aIUnit;

    public VictoryState(AIUnit aIUnit)
    {
        this.aIUnit = aIUnit;
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public void Stay()
    {
        throw new System.NotImplementedException();
    }

    public void Enter()
    {
        throw new System.NotImplementedException();
    }
}
