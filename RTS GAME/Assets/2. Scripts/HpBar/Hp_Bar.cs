using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_Bar : MonoBehaviour
{
    public bool isdead = false; // 생존 여부를 판단하는 bool 변수
    public Slider hpbar; // 체력바 UI
    public float maxHp; // 최대 체력
    public float currenthp; // 현재 체력

    void Update()
    {
        transform.position = this.transform.position + new Vector3(0, 0, 0); // 체력바 위치 설정
        if (hpbar != null) // 체력바 UI가 존재할 때
            hpbar.value = currenthp; // 체력바 값 설정
        if (currenthp <= 0) // 현재 체력이 0 이하일 때
        {
            isdead = true; // 생존 여부를 false에서 true로 변경
        }
    }

    public void GetAttack(float damage, float Armor)
    {
        float realdamage = damage - Armor; // 데미지에서 방어력을 뺀 실제 데미지 계산
        if (realdamage <= 0) // 실제 데미지가 0 이하일 때
        {
            realdamage = 1; // 최소 데미지를 1로 설정
        }
        currenthp -= realdamage; // 현재 체력에서 데미지만큼 감소
    }

    public void SetMaxHP()
    {
        hpbar.maxValue = maxHp; // 체력바의 최대값 설정
    }
}