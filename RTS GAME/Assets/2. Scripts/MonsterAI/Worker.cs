using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using System;

public class Worker : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target = null;
    private Animator animator;
    private GameObject mytower;

    private bool IsWork = false;
    private bool Mined = false;
    private bool IsBig = false;
    private string crystal_name;
    // Start is called before the first frame update
    void Awake()
    {
        crystal_name = "small crystal target";
        
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GameObject.Find(crystal_name).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
            agent.SetDestination(target.position);
        if (!IsWork)
            animator.SetBool("IsMove", true);
    }

    public void Settarget(Transform tf)
    {
        target = tf;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        // 크리스탈에 도착하여 자원 캐기
        if (collision.gameObject.tag == "Crystal")
        {
            IsWork = true;
            animator.SetBool("IsMove", false);
            animator.SetBool("IsWork", true);
            StartCoroutine("pickup");
        }
        // 자원을 캔 후 타워로 타겟1  `2`  지정
        if (collision.gameObject.tag == "Store_Tower")
        {
            Debug.Log("Store");
            if (Mined)
            {
                if (GameObject.Find("Mine WallTower").GetComponent<Tower>().upgrade_lv >= 3)
                    BigCrystal();
                if (IsBig)
                    crystal.instance.now_crystal += 2;
                else
                    crystal.instance.now_crystal += 1;
                Mined = false;
                Settarget(GameObject.Find(crystal_name).transform);
            }
        }
    }

    IEnumerator pickup()
    {
        yield return new WaitForSeconds(6.0f);
        animator.SetTrigger("Pickup");
        yield return new WaitForSeconds(0.5f);
        IsWork = false;
        animator.SetBool("IsMove", true);
        Settarget(GameObject.Find("Mine StoreTower").transform);
        Mined = true;
    }

    public void BigCrystal()
    {
        crystal_name = "big crystal target";
        IsBig = true;
    }
}