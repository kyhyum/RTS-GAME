using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_Bar : MonoBehaviour
{
    private Transform player;
    public int num = 0;
    public Slider hpbar;
    public float maxHp;
    public float currenthp;

    void Awake()
    {
        player = GetComponent<Transform>();
    }
    void Update()
    {
        transform.position = player.position + new Vector3(0, 0, 0);
        if(hpbar != null)
            hpbar.value = currenthp;
        if(currenthp <= 0)
        {
            UnitSpawn.instance.Die(player.gameObject, num);
        }
    }
    public void hpchg()
    {
        hpbar.maxValue = maxHp;
    }
}
