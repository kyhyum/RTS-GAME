using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryState : IState
{
    private AIUnit Units;

    public VictoryState(AIUnit aIUnit)
    {
        Units = aIUnit;
    }

    public void Exit()
    {
        
    }

    public void Stay()
    {
    }

    public void Enter()
    {
        Units.PlayAnimation(AIUnit.State.Victory);
    }
}
