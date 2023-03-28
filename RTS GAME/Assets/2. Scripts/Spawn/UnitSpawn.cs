using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawn : MonoBehaviour
{
    Hpbar_Control hpbar_Control;

    Transform Canvas_Position;
    public string unit_name = "Human";
    //Hpbar 프리펩
    public GameObject Player_HPbar;
    public GameObject Enemy_HPbar;
    // 종족별 유닛들 프리팹
    public GameObject[] hunit = new GameObject[8];
    public GameObject[] sunit = new GameObject[8];
    public GameObject[] uunit = new GameObject[8];
    public GameObject[] wunit = new GameObject[8];

    // 나의 종족 프리팹
    private GameObject[] unit = new GameObject[8];

    // 오브젝트 풀링
    List<Queue<GameObject>> unit_queue = new List<Queue<GameObject>>();
    Queue<GameObject> Player_Hp_bar_queue = new Queue<GameObject>();
    Queue<GameObject> Enemy_Hp_bar_queue = new Queue<GameObject>();

    void Awake()
    {
        hpbar_Control = GameObject.Find("Canvas").GetComponent<Hpbar_Control>();
        if (unit_name == "Human")
        {
            for(int i = 0; i < 8; i++)
            {
                Queue<GameObject> q = new Queue<GameObject>();
                unit_queue.Add(q);
                unit[i] = hunit[i];
            }
        }
        for(int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject Unit = CreateUnit(j);
                GameObject Hpbar = Create_HPbar(Player_HPbar);
                Unit.GetComponent<Hp_Bar>().hpbar = Hpbar.GetComponent<Slider>();
                hpbar_Control.obj.Add(Unit.transform);
                hpbar_Control.hp_bar.Add(Hpbar);
                unit_queue[j].Enqueue(Unit);
                Player_Hp_bar_queue.Enqueue(Hpbar);
            }
        }
    }
    private GameObject Create_HPbar(GameObject gameObject)
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawn(int n)
    {
        if(unit_queue[n].Count == 0)
            unit_queue[n].Enqueue(CreateUnit(n));

        GameObject unit = unit_queue[n].Dequeue();
        
        unit.transform.position = new Vector3(Random.RandomRange(-8, 8), 7, -26);
        unit.SetActive(true);
    }
}
