using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetHitState : IState
{
    private AIUnit Units;

    public GetHitState(AIUnit aIUnit)
    {
        Units = aIUnit;
    }

    public void Enter()
    {

    }
    public void Stay()
    {
        throw new System.NotImplementedException();
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }

}
