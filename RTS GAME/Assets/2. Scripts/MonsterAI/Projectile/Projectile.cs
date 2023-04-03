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
        // Short delay added before Projectile is thrown
        yield return new WaitForSeconds(1.5f);

        // Move projectile to the position of throwing object + add some offset if needed.
        Projectile_trs.position = myTransform.position + new Vector3(0, 0.0f, 0);

        // Calculate distance to target
        float target_Distance = Vector3.Distance(Projectile_trs.position, Target.position);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        Projectile_trs.rotation = Quaternion.LookRotation(Target.position - Projectile_trs.position);

        float elapse_time = 0;

        while (elapse_time < flightDuration)
        {
            Projectile_trs.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

            elapse_time += Time.deltaTime;

            yield return null;
        }

        projectile_Spawn.Unactive_Projectile(this.gameObject);
        hp_bar.GetAttack(damage, armor);
    }
}


