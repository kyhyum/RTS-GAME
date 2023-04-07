using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

public class PathRequestManager : MonoBehaviour
{
    // 현재 경로 요청을 저장하는 큐
    Queue<PathResult> results = new Queue<PathResult>();

    static PathRequestManager instance;
    Pathfinding pathfinding;

    void Awake()
    {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
    }

    void Update()
    {
        // 경로 요청이 있는 경우
        if (results.Count > 0)
        {
            int itemsInQueue = results.Count;
            lock (results)
            {
                // 저장된 경로 요청을 하나씩 처리
                for (int i = 0; i < itemsInQueue; i++)
                {
                    PathResult result = results.Dequeue();
                    result.callback(result.path, result.success);
                }
            }
        }
    }

    // 경로 요청을 받아 경로 탐색 스레드를 실행하는 메서드
    public static void RequestPath(PathRequest request)
    {
        // 스레드 생성 및 실행
        ThreadStart threadStart = delegate {
            instance.pathfinding.FindPath(request, instance.FinishedProcessingPath);
        };
        threadStart.Invoke();
    }

    // 경로 탐색이 완료된 경로를 큐에 저장하는 콜백 메서드
    public void FinishedProcessingPath(PathResult result)
    {
        // 경로 요청 큐에 결과 저장
        lock (results)
        {
            results.Enqueue(result);
        }
    }
}
// 경로 요청의 결과를 담는 구조체
public struct PathResult
{
    public Vector3[] path; // 찾은 경로
    public bool success; // 경로 찾기가 성공했는지 여부
    public Action<Vector3[], bool> callback; // 경로 요청 완료 후 호출할 콜백 함수
    public PathResult(Vector3[] path, bool success, Action<Vector3[], bool> callback)
    {
        this.path = path;
        this.success = success;
        this.callback = callback;
    }
}

// 경로 요청을 담는 작은 구조체
public struct PathRequest
{
    public Vector3 pathStart; // 경로 시작점
    public Vector3 pathEnd; // 경로 끝점
    public Action<Vector3[], bool> callback; // 경로 찾기 완료 후 호출할 콜백 함수
    public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback)
    {
        pathStart = _start;
        pathEnd = _end;
        callback = _callback;
    }
}