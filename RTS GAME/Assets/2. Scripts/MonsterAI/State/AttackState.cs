using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AttackState : IState
{
    private AIUnit Units;
    AIUnit target_AIUnit;

    public AttackState(AIUnit aIUnit)
    {
        Units = aIUnit;
    }
    public void Enter()
    {
        Units.transform.rotation = Quaternion.LookRotation(Units.target.position - Units.transform.position);
        target_AIUnit = Units.target.GetComponent<AIUnit>();
    }
    public void Stay()
    {
        Units.PlayAnimation(AIUnit.State.Attack);
        if (Units.is_range_long)
        {
            //투사체 오브젝트 풀링 받아와서 쏘기
        }
        target_AIUnit.hp_bar.GetAttack(Units.attack, target_AIUnit.armor);
        float distance = Vector3.Distance(Units.transform.position, Units.target.position);
        if(distance > Units.attackRange + 1)
        {
            Units.States = AIUnit.State.Walk;
            Units.target = Units.DefualtTarget;
        }
    }

    public void Exit()
    {
    }



}
