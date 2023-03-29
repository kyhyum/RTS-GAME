using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IState
{
    private AIUnit Units;
    float time = 0;

    public DeadState(AIUnit aIUnit)
    {
        Units = aIUnit;
    }

    public void Enter()
    {
        Debug.Log("Enter");
        Units.PlayAnimation(AIUnit.State.Death);
        Units.isCreep = false;
    }

    public void Exit()
    {
        UnitSpawn.instance.Die(Units.gameObject, Units.character_num);
    }

    public void Stay()
    {
        time += Time.deltaTime;
        if (time >= 1.6f)
        {
            Units.States = AIUnit.State.Idle;
        }

    }
}