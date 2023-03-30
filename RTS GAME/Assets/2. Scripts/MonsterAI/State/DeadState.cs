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
        //상태 진입 시 Death 애니메이션 실행
        Units.PlayAnimation(AIUnit.State.Death);
        //활성화 false로 초기화
        Units.isEnable = false;
    }

    public void Exit()
    {
        //idle 상태로 변함과 동시에 unitspawn 함수에서 오브젝트 풀링 처리
        UnitSpawn.instance.Die(Units.gameObject, Units.character_num);
    }

    public void Stay()
    {
        //death 애니메이션 실행 후 idle로 상태 변환
        time += Time.deltaTime;
        if (time >= 1.6f)
        {
            Units.States = AIUnit.State.Idle;
        }

    }
}