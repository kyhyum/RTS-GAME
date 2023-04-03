using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Worker : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform target = null;
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
            agent.SetDestination(target.position);
    }

    public void Settarget(Transform tf)
    {
        target = tf;
    }
}
