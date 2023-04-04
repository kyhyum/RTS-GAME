using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackState : MonoBehaviour,IState
{
    Hp_Bar target_hp_bar;
    private AIUnit Units;
    Tower Target_Tower;
    AIUnit target_AIUnit;
    float time;
    bool istower = false;
    Projectile_Spawn projectile_Spawn;

    public AttackState(AIUnit aIUnit)
    {
        Units = aIUnit;
    }
    public void Enter()
    {
        //시간
        time = 0;
        //상태 진입시 상대 오브젝트를 향해 회전
        Units.transform.rotation = Quaternion.LookRotation(Units.target.position - Units.transform.position);
      

        if (Units.is_range_long)
        {
            projectile_Spawn = Units.GetComponent<Projectile_Spawn>();
        }

        if (Units.target.CompareTag(Units.DefaultTarget.tag))
        {
            Target_Tower = Units.target.GetComponent<Tower>();
            target_hp_bar = Target_Tower.GetComponent<Hp_Bar>();
            istower = true;
        }
        else
        {
            //상대의 AIUnit 스크립트를 target_AiUnit에 받아옴
            target_AIUnit = Units.target.GetComponent<AIUnit>();
            target_hp_bar = target_AIUnit.GetComponent<Hp_Bar>();
        }
    }
    public void Stay()
    {
        if (!Units.isDead || !target_AIUnit.isDead)
        {
            if (time == 0)
            {
                if(target_AIUnit.isInAir)
                {
                    //Air_Attack 애니메이션 실행
                    Units.PlayAnimation(AIUnit.State.Air_Attack);
                }
                else
                {
                    //Attack 애니메이션을 실행
                    Units.PlayAnimation(AIUnit.State.Attack);
                }
            }

            //1.2초마다 상대 피가 까이고 오브젝트가 날아가게 함
            time += Time.deltaTime;

            if (time >= Units.attack_anim_speed)
            {
                if (Units.is_range_long)
                {
                    //투사체 오브젝트 풀링 받아와서 쏘기
                    GameObject projectile = projectile_Spawn.Spawn_Projectile(Units.transform, Units.target.transform);
                    float dist = Vector3.Distance(Units.transform.position, Units.target.transform.position);
                    Projectile projectile_com = projectile.GetComponent<Projectile>();
                    if(istower) projectile_com.Launch(Units.Weapon, Units.target, dist, 0.5f, target_hp_bar,Units.attack, Target_Tower.armor);
                    else projectile_com.Launch(Units.Weapon, Units.target, dist, 0.5f, target_hp_bar, Units.attack, target_AIUnit.armor);
                    projectile_com.projectile_Spawn = projectile_Spawn;
                }
                else
                {
                    if (istower) target_hp_bar.GetAttack(Units.attack, Target_Tower.armor);                
                    else target_hp_bar.GetAttack(Units.attack, target_AIUnit.armor);
                }
                time = 0;

            }        
        }
        //적이 죽거나 적이 거리가 멀어지면 walkstate로 변환
        if (!istower)
        {
            float distance = Vector3.Distance(Units.transform.position, Units.target.position);
            if (distance > Units.attackRange + 1 || target_AIUnit.isDead == true)
            {
                Units.States = AIUnit.State.Walk;
                Units.target = Units.DefaultTarget;
                Units.unit.target = Units.DefaultTarget;
            }
        }
    }

    public void Exit()
    {
    }
}