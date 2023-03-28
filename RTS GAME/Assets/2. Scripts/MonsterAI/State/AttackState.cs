using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AttackState : IState
{
    private AIUnit Units;

    public AttackState(AIUnit aIUnit)
    {
        Units = aIUnit;
    }
    public void Enter()
    {
        Quaternion targetRotation = Quaternion.LookRotation(Units.target.position - Units.transform.position);
        Units.transform.rotation = Quaternion.Lerp(Units.transform.rotation, targetRotation, Time.deltaTime * 1);

    }
    public void Stay()
    {
        Units.PlayAnimation(AIUnit.State.Attack);
        float distance = Vector3.Distance(Units.transform.position, Units.target.position);
        if(distance > Units.seekRange)
        {
            Units.States = AIUnit.State.Walk;
            Units.target = Units.DefualtTarget;
        }
    }

    public void Exit()
    {
    }



}
