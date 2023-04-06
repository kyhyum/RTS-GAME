using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_Bar : MonoBehaviour
{
    public bool isdead = false;
    public Slider hpbar;
    public float maxHp;
    public float currenthp;

    void Awake()
    {
    }
    void Update()
    {
        transform.position = this.transform.position + new Vector3(0, 0, 0);
        if (hpbar != null)
            hpbar.value = currenthp;
        if (currenthp <= 0)
        {
            isdead = true;
        }
    }
    public void GetAttack(float damage, float Armor)
    {
        float realdamage = damage - Armor;
        if (realdamage <= 0)
        {
            realdamage = 1;
        }
        currenthp -= realdamage;
    }
    public void SetMaxHP()
    {
        hpbar.maxValue = maxHp;
    }
}