using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawn : MonoBehaviour
{
   // public Transform DefaultTarget;

    public static UnitSpawn instance = null;
    Hpbar_Control hpbar_Control;
    public int spawn_unit_num = -1;

    public Transform DefaultTarget;

    public Camera ca;
    public Transform Canvas_Position;
    public Transform UI_Pos;
    public GameObject SpawnPos;
    private string unit_name = "Human";
    //Hpbar 프리펩
    public GameObject Player_HPbar;
    public GameObject Enemy_HPbar;
    // 종족별 유닛들 프리팹
    public GameObject[] unitUI = new GameObject[4];
    public GameObject[] hunit = new GameObject[8];
    public GameObject[] sunit = new GameObject[8];
    public GameObject[] uunit = new GameObject[8];
    public GameObject[] wunit = new GameObject[8];

    // 나의 종족 프리팹
    private GameObject MyUnitUI;
    public GameObject[] unit = new GameObject[8];

    // 오브젝트 풀링
    List<Queue<GameObject>> unit_queue = new List<Queue<GameObject>>();
    Queue<GameObject> Player_Hp_bar_queue = new Queue<GameObject>();
    Queue<GameObject> Enemy_Hp_bar_queue = new Queue<GameObject>();

    void Awake()
    {
        instance = this;
        hpbar_Control = GameObject.Find("Canvas").GetComponent<Hpbar_Control>();

        for (int i = 0; i < 8; i++)
        {
            Queue<GameObject> q = new Queue<GameObject>();
            unit_queue.Add(q);
        }

        if (unit_name == "Human")
        {
            unit = hunit;
            MyUnitUI = Instantiate(unitUI[0], Vector2.zero, Quaternion.identity, UI_Pos.Find("Viewport"));
        }
        else if (unit_name == "Sentinel")
        {
            unit = sunit;
            MyUnitUI = Instantiate(unitUI[1], Vector2.zero, Quaternion.identity, UI_Pos.Find("Viewport"));
        }
        else if (unit_name == "Undead")
        {
            unit = uunit;
            MyUnitUI = Instantiate(unitUI[2], Vector2.zero, Quaternion.identity, UI_Pos.Find("Viewport"));
        }
        else
        {
            unit = wunit;
            MyUnitUI = Instantiate(unitUI[3], Vector2.zero, Quaternion.identity, UI_Pos.Find("Viewport"));
        }
        RectTransform UIrt = MyUnitUI.GetComponent<RectTransform>();
        UI_Pos.GetComponent<ScrollRect>().content = UIrt;
        MyUnitUI.transform.position = new Vector3(MyUnitUI.transform.position.x, 100, MyUnitUI.transform.position.z);

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject Unit = CreateUnit(j);
                Unit.GetComponent<AIUnit>().Settarget(DefaultTarget);
                
                GameObject Hpbar = Create_HPbar(Player_HPbar, Unit);

                Hp_Bar hp_bar = Unit.GetComponent<Hp_Bar>();
                hp_bar.hpbar = Hpbar.GetComponent<Slider>();
                hp_bar.SetMaxHP();

                hpbar_Control.obj.Add(Unit.transform);
                hpbar_Control.hp_bar.Add(Hpbar);
                unit_queue[j].Enqueue(Unit);
                Player_Hp_bar_queue.Enqueue(Hpbar);
            }
        }
    }
    void Update()
    {
        if (spawn_unit_num != -1)
            SpawnPos.SetActive(true);
        else
            SpawnPos.SetActive(false);
    }
    private GameObject Create_HPbar(GameObject gameObject, GameObject player)
    {
        GameObject Hp_bar = Instantiate(gameObject, Vector2.zero, Quaternion.identity, Canvas_Position);
        Hp_bar.SetActive(false);
        return Hp_bar;
    }
    private GameObject CreateUnit(int n)
    {
        GameObject unit_object = Instantiate(unit[n]);
        unit_object.SetActive(false);
        return unit_object;
    }
    

    public void spawn(int n)
    {
        /*
        Debug.Log(n);
        Debug.Log(unit_queue.Count);
        if (unit_queue[n].Count == 0)
            unit_queue[n].Enqueue(CreateUnit(n));

        GameObject unit = unit_queue[n].Dequeue();

        // 수정
        unit.transform.position = new Vector3(Random.RandomRange(-8, 8), 7, -26);
        unit.SetActive(true);
        */

        SpawnClear(n);
        if (spawn_unit_num == n)
            spawn_unit_num = -1;
        else
            spawn_unit_num = n;


    }

    private void SpawnClear(int n)
    {
        Transform tf = UI_Pos.Find("Viewport").Find(unit_name + "_Unit(Clone)");
        for (int i = 1; i < 9; i++)
        {
            if (i == n + 1)
                continue;
            Color color = tf.GetChild(i).gameObject.GetComponent<Image>().color;

            color.a = 1f;
            tf.GetChild(i).gameObject.GetComponent<Image>().color = color;

        }
    }

    public void Die(GameObject unit_object, int n)
    {
        // 몬스터 사망 시 큐에 추가
        unit_object.SetActive(false);
        unit_queue[n].Enqueue(unit_object);

    }

    public void SpawnUnit(int n)
    {
        if (spawn_unit_num == -1)
            return;
        if (unit_queue[spawn_unit_num].Count == 0)
            unit_queue[spawn_unit_num].Enqueue(CreateUnit(spawn_unit_num));
        Vector3 vec = new Vector3(-9 + n * 6.25f, 0, -25);
        GameObject unit = unit_queue[spawn_unit_num].Dequeue();

        // 수정

        unit.transform.position = vec;
        unit.SetActive(true);
    }
}