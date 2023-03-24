using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawn : MonoBehaviour
{
    public string unit_name = "Human";
    // 종족별 유닛들 프리팹
    public GameObject[] hunit = new GameObject[8];

    // 나의 종족 프리팹
    private GameObject[] unit = new GameObject[8];

    // 오브젝트 풀링
    List<Queue<GameObject>> unit_queue = new List<Queue<GameObject>>();

    void Awake()
    {
        if(unit_name == "Human")
        {
            for(int i = 0; i < 8; i++)
            {
                Queue<GameObject> q = new Queue<GameObject>();
                unit_queue.Add(q);
                unit[i] = hunit[i];
            }
        }
        for(int i = 0; i < 20; i++)
        {
            for (int j = 0; j < 8; j++)
                unit_queue[j].Enqueue(CreateUnit(j));
        }
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
