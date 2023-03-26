using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAI : MonoBehaviour
{
    public Transform target;
    float attackDelay;

    private SphereCollider attackRange;
    private Unit unit;


    void Awake()
    {
        unit = GetComponent<Unit>();
        attackRange = GetComponent<SphereCollider>();    
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Stop");
            unit.StopMethod();
        }
    }

    void FaceTarget(GameObject gameobject)
    {
            transform.localScale = new Vector3(gameobject.transform.position.x - transform.position.x, gameobject.transform.position.y - transform.position.y, 1);
    }
}
