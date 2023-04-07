using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hpbar_Control : MonoBehaviour
{
    // 캐릭터(Obj)와 체력바(hp_bar)를 각각 리스트에 담아서 관리합니다.
    public List<Transform> obj;
    public List<GameObject> hp_bar;

    Camera camera;

    // Start 함수에서는 초기 위치를 설정합니다.
    void Start()
    {
        camera = Camera.main;
        for (int i = 0; i < obj.Count; i++)
        {
            hp_bar[i].transform.position = obj[i].position;
        }
    }

    // Update 함수에서는 카메라 위치를 기준으로 체력바 위치를 업데이트합니다.
    void Update()
    {
        for (int i = 0; i < obj.Count; i++)
        {
            hp_bar[i].transform.position = camera.WorldToScreenPoint(obj[i].position + new Vector3(0, 3f, 0));
        }
    }
}