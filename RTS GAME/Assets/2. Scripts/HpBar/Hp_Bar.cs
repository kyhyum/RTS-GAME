using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_Bar : MonoBehaviour
{
    private Transform player;
    public Slider hpbar;
    public float maxHp;
    public float currenthp;

    void Awake()
    {
        player = GetComponent<Transform>();
        hpbar.maxValue = maxHp;
    }
    void Update()
    {
        transform.position = player.position + new Vector3(0, 0, 0);
        hpbar.value = currenthp;
    }
}
