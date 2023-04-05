using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class WalkState : IState
{
    private AIUnit Units;

    public WalkState(AIUnit aIUnit)
    {
        Units = aIUnit;
    }


    public void Enter()
    {
        Units.PlayAnimation(AIUnit.State.Walk);
        Units.unit.target = Units.target.transform;
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
        //attackRange만큼의 구에서 Layer가 Enemy인 콜라이더를 찾음
        Collider[] colliders = Physics.OverlapSphere(Units.transform.position, Units.attackRange, 1 << LayerMask.NameToLayer(Units.Opposite_team));
        if (colliders.Length <= 0) return;
        float minDist = Mathf.Infinity; // 가장 가까운 적과의 거리를 저장하기 위한 변수
        Transform neareastTarget = null; // 가장 가까운 적을 저장하기 위한 변수
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].tag == "Enemy_Tower")
            {
                Tower towers = colliders[i].GetComponentInParent<Tower>();
                float dist = Vector3.Distance(Units.transform.position, towers.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    neareastTarget = towers.transform;
                }
                continue;
            }

            AIUnit temp = colliders[i].GetComponentInParent<AIUnit>();
            if (!temp.isDead)
            {
                if(temp.isInAir && !Units.AIr_Unit_Attack)
                {
                    continue;
                }
                //적 중에서 가장 가까운 타깃을 찾음
                float dist = Vector3.Distance(Units.transform.position, temp.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    neareastTarget = temp.transform;
                }
            }
        }
        if (neareastTarget != null)
        {
            Units.target = neareastTarget;
            Units.States = AIUnit.State.Attack;
        }
    }

}