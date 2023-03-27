using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private AIUnit aIUnit;

    public AttackState(AIUnit aIUnit)
    {
        this.aIUnit = aIUnit;
    }
    public void Enter()
    {
        throw new System.NotImplementedException();
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
