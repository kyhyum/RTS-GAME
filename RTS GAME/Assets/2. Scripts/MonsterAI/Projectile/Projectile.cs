using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    bool Launched = false;
    Hp_Bar hp_bar;
    float damage;
    float armor;
    Transform Target;
    Transform Unit;
    float Speed;
    public float height;
    public Projectile_Spawn projectile_Spawn;

    public void Launch(Transform _unit, Transform _target, float _speed, float _height, Hp_Bar _hp_Bar, float _damage, float _armor)
    {
        damage = _damage;
        armor= _armor;
        hp_bar = _hp_Bar;
        Unit = _unit;
        Target = _target;
        Speed = _speed;
        height= _height;
        this.gameObject.transform.position = new Vector3(_unit.position.x,height,_unit.position.z);
        Launched = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(Launched == true)
        {
            Vector3 TargetPosition = Target.position;
            TargetPosition.y = height;

            transform.LookAt(TargetPosition,Vector3.up);
            Debug.Log(this.gameObject.transform.rotation);
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
}
