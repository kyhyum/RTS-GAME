using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    public static EnemySpawn instance = null;
    public Hpbar_Control hpbar_Control;
    public int spawn_unit_num = -1;

    public Transform DefaultTarget;
    public Tower MyTower;

    public Transform Canvas_Position;
    public string enemy_unit_name = "Human";
    //Hpbar 프리펩
    public GameObject Enemy_HPbar;
    // 종족별 유닛들 프리팹
    public GameObject[] enemy_hunit = new GameObject[8];
    public GameObject[] enemy_sunit = new GameObject[8];
    public GameObject[] enemy_uunit = new GameObject[8];
    public GameObject[] enemy_wunit = new GameObject[8];

    // 나의 종족 프리팹
    public GameObject[] unit = new GameObject[8];

    // 오브젝트 풀링
    public List<Queue<GameObject>> enemy_unit_queue = new List<Queue<GameObject>>();

    //업그레이드 수치
    public int enemy_PlusAttack = 0, enemy_PlusArmor = 0;

    void Awake()
    {
        instance = this;
        hpbar_Control = hpbar_Control.GetComponent<Hpbar_Control>();
        MyTower = MyTower.GetComponent<Tower>();

        for (int i = 0; i < 8; i++)
        {
            Queue<GameObject> q = new Queue<GameObject>();
            enemy_unit_queue.Add(q);
        }

        if (enemy_unit_name == "Human")
        {
            unit = enemy_hunit;
        }
        else if (enemy_unit_name == "Sentinel")
        {
            unit = enemy_sunit;
        }
        else if (enemy_unit_name == "Undead")
        {
            unit = enemy_uunit;
        }
        else
        {
            unit = enemy_wunit;
        }

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                CreateUnit(j);
            }
        }
    }

    private GameObject Create_HPbar(GameObject gameObject, GameObject player)
    {
        GameObject Hp_bar = Instantiate(gameObject, Vector2.zero, Quaternion.identity, Canvas_Position);
        Hp_bar.SetActive(false);
        return Hp_bar;
    }
    private void CreateUnit(int n)
    {
        GameObject Unit = Instantiate(unit[n]);
        Unit.SetActive(false);

        if(n != 0)
        {
            AIUnit Units = Unit.GetComponent<AIUnit>();
            Units.Settarget(DefaultTarget);
            Units.MyTower = MyTower;
        }

        GameObject Hpbar = Create_HPbar(Enemy_HPbar, Unit);

        Hp_Bar hp_bar = Unit.GetComponent<Hp_Bar>();
        hp_bar.hpbar = Hpbar.GetComponent<Slider>();
        hp_bar.SetMaxHP();

        hpbar_Control.obj.Add(Unit.transform);
        hpbar_Control.hp_bar.Add(Hpbar);
        enemy_unit_queue[n].Enqueue(Unit);
    }


    public void Enemy_Die(GameObject unit_object, int n)
    {
        unit_object.SetActive(false);
        enemy_unit_queue[n].Enqueue(unit_object);
    }

    public void Spawn_Enemy_Unit(int n, int spawn_Enemyunit_num)
    {
        if (enemy_unit_queue[spawn_Enemyunit_num].Count == 0)
            CreateUnit(spawn_Enemyunit_num);
        Vector3 vec = new Vector3(9 + n * -6.25f, 0, 23);
        GameObject unit = enemy_unit_queue[spawn_Enemyunit_num].Dequeue();
        if (spawn_Enemyunit_num != 0)
        {
            unit.GetComponent<AIUnit>().armor += enemy_PlusArmor;
            unit.GetComponent<AIUnit>().attack += enemy_PlusAttack;
        }
        Hp_Bar hp_bar = unit.GetComponent<Hp_Bar>();
        hp_bar.hpbar.gameObject.SetActive(true);
        unit.transform.position = vec;
        unit.SetActive(true);
    }

}