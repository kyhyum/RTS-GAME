using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIChange : MonoBehaviour
{
    public GameObject UpgradeUI;
    public Transform ViewPort;
    Transform UnitUI;

    //업그레이드 비용 
    private int AttackCost, ArmorCost, TowerCost;

    //타워 업그레이드 메테리얼
    public Material lv2, lv3, lv4;
    private int tower_lv = 1, attack_lv = 1, armor_lv = 1;

    // 업그레이드 레벨 텍스트
    public TMP_Text tower, attack, armor; 

    // Start is called before the first frame update
    void Awake()
    {
        AttackCost = 30;
        ArmorCost = 30;
        TowerCost = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnitClick()
    {
        UpgradeUI.SetActive(false);
        ViewPort.Find(UnitSpawn.instance.unit_name + "_Unit(Clone)").gameObject.SetActive(true);
    }
    public void AttackUpgrade()
    {
        //공격 업그레이드
        if(AttackCost > crystal.instance.now_crystal)
        {
            UnitSpawn.instance.PriceLack();
            return;
        }
        attack.text = (++attack_lv).ToString();
        crystal.instance.now_crystal -= AttackCost;
        UnitSpawn.instance.PlusAttack += 1;


    }
    public void ArmorUpgrade()
    {
        //아머 업그레이드
        if (ArmorCost > crystal.instance.now_crystal)
        {
            UnitSpawn.instance.PriceLack();
            return;
        }
        armor.text = (++armor_lv).ToString();
        crystal.instance.now_crystal -= ArmorCost;
        UnitSpawn.instance.PlusArmor += 1;

    }
    public void TowerUpgrade()
    {
        //타워 업그레이드
        if (tower_lv == 4)
            return;
        if (TowerCost > crystal.instance.now_crystal)
        {
            UnitSpawn.instance.PriceLack();
            return;
        }
        crystal.instance.now_crystal -= TowerCost;
        GameObject mytower = GameObject.Find("Mine WallTower");
        if (tower_lv == 1)
        {
            mytower.GetComponent<MeshRenderer>().material = lv2;
        }
        else if(tower_lv == 2)
        {
            mytower.GetComponent<MeshRenderer>().material = lv3;
        }
        else
        {
            mytower.GetComponent<MeshRenderer>().material = lv4;
            UpgradeUI.transform.GetChild(1).GetComponent<Button>().interactable = false;
        }
        mytower.GetComponent<Tower>().upgrade_lv++;
        mytower.GetComponent<Hp_Bar>().maxHp += 50;
        mytower.GetComponent<Hp_Bar>().currenthp += 50;
        tower.text = (++tower_lv).ToString();
        
    }

}
