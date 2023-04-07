using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    public bool isparabola;
    bool Launched = false;
    Hp_Bar hp_bar;
    float damage;
    float armor;
    Transform Target;
    float Speed;
    public float height;
    public Projectile_Spawn projectile_Spawn;

    private float firingAngle = 45.0f;
    private float gravity = 9.8f;

    public Transform Projectile_trs;
    private Transform myTransform;

    private void Awake()
    {
        myTransform = transform;
    }

    public void Launch(Transform _unit, Transform _target, float _speed, float _height, Hp_Bar _hp_Bar, float _damage, float _armor)
    {
        damage = _damage;
        armor= _armor;
        hp_bar = _hp_Bar;
        Target = _target;
        Speed = _speed;
        height= _height;
        this.gameObject.transform.position = new Vector3(_unit.position.x,height,_unit.position.z);

        if (isparabola)
        {
            StartCoroutine(SimulateProjectile());
        }
        else
        {
            Launched = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Launched == true)
        {
            Vector3 TargetPosition = Target.position;
            TargetPosition.y = height;

            transform.LookAt(TargetPosition,Vector3.up);
            this.transform.eulerAngles = new Vector3(90, this.transform.eulerAngles.y, this.transform.eulerAngles.z);

            Vector3 NewPosition = Vector3.MoveTowards(transform.position, TargetPosition, Speed * Time.deltaTime);
            transform.position = NewPosition;

            if(this.transform.position == TargetPosition)
            {
                Launched = false;
                projectile_Spawn.Unactive_Projectile(this.gameObject);
                hp_bar.GetAttack(damage, armor);
            }
        }
    }

    IEnumerator SimulateProjectile()
    {
        yield return new WaitForSeconds(1.5f); // 대기 시간

        Projectile_trs.position = myTransform.position + new Vector3(0, 0.0f, 0); // 발사체 위치 설정

        float target_Distance = Vector3.Distance(Projectile_trs.position, Target.position); // 발사 대상과의 거리 계산

        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity); // 발사체의 속도 계산

        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad); // 발사체의 x축 속도 계산
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad); // 발사체의 y축 속도 계산

        float flightDuration = target_Distance / Vx; // 발사체 비행 시간 계산

        Projectile_trs.rotation = Quaternion.LookRotation(Target.position - Projectile_trs.position); // 발사체 방향 설정

        float elapse_time = 0; // 경과 시간 초기화

        while (elapse_time < flightDuration)
        {
            Projectile_trs.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime); // 발사체 위치 변경

            elapse_time += Time.deltaTime; // 경과 시간 증가

            yield return null; // 다음 프레임까지 대기
        }

        projectile_Spawn.Unactive_Projectile(this.gameObject); // 발사체 비활성화
        hp_bar.GetAttack(damage, armor); // 대미지 적용
    }
}


