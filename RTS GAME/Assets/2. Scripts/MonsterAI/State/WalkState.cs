using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WalkState : IState
{
    private AIUnit Units;
    bool isGetAttacked = false;

    public WalkState(AIUnit aIUnit)
    {
        Units = aIUnit;
    }


    public void Enter()
    {
        Units.PlayAnimation(AIUnit.State.Walk);
        Units.unit.target = Units.target;
        Units.unit.StartMethod();
    }

    public void Stay()
    {
        FindTarget();
    }

    public void Exit()
    {
        Units.unit.StopMethod();
    }



    public void FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(Units.transform.position, Units.seekRange, 1 << LayerMask.NameToLayer(Units.Opposite_team));
        if (colliders.Length <= 0) return;
        AIUnit neareastTarget = null; // 가장 가까운 적을 저장하기 위한 변수
        float minDist = Mathf.Infinity; // 가장 가까운 적과의 거리를 저장하기 위한 변수
        for(int i = 0; i < colliders.Length; i++)
        {
            AIUnit temp = colliders[i].GetComponentInParent<AIUnit>();
            if(!temp.isDead)
            {
                float dist = Vector3.Distance(Units.transform.position, temp.transform.position);
                if(dist < minDist)
                {
                    minDist = dist;
                    neareastTarget = temp;
                }
            }
        }
        if(neareastTarget != null)
        {
            Units.unit.StopMethod();
            Units.unit.target = neareastTarget.transform;
            Units.target = neareastTarget.transform;
            Units.unit.StartMethod();
            float distance = Vector3.Distance(Units.transform.position, neareastTarget.transform.position);
            if (distance < Units.attackRange)
            {
                Units.unit.StopMethod();
                Units.States = AIUnit.State.Attack;
            }
        }
    }

}
