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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Enemy"))
        {
            //범위에 들어온 적으로 공격 지정
            //unit.target = collision.collider.gameObject.transform;
            //회전
            //FaceTarget(collsion.collider.gameObject);
            //A*알고리즘 정지
            //unit.StopMethod();
            //공격 애니메이션
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Enemy"))
        {
            //타워로 공격 지정
            //unit.target = ;
            //A* 알고리즘 다시 실행
            //unit.StartMethod();
            //걷는 애니메이션
        }
    }

    void FaceTarget(GameObject gameobject)
    {
            transform.localScale = new Vector3(gameobject.transform.position.x - transform.position.x, gameobject.transform.position.y - transform.position.y, 1);
    }
}
