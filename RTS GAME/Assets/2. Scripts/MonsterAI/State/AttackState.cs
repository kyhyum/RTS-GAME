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
        Debug.Log("Attack");
        Units.PlayAnimation(AIUnit.State.Attack);
    }

    public void Exit()
    {
    }



}
