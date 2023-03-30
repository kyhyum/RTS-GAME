using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        Collider[] colliders = Physics.OverlapSphere(Units.transform.position, Units.attackRange, 1 << LayerMask.NameToLayer(Units.Opposite_team));
        if (colliders.Length <= 0) return;
        float minDist = Mathf.Infinity; // 가장 가까운 적과의 거리를 저장하기 위한 변수
        AIUnit neareastTarget = null; // 가장 가까운 적을 저장하기 위한 변수
        for (int i = 0; i < colliders.Length; i++)
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
            Debug.Log("have target");
            Units.target = neareastTarget.transform;
            Units.States = AIUnit.State.Attack;
        }
    }

}
