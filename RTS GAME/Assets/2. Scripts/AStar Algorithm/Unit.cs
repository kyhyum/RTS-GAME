using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    // 최소 경로 업데이트 시간
    const float minPathUpdateTime = .2f;
    // 경로 업데이트 거리 임계치
    const float pathUpdateMoveThreshold = .5f;

    // 목표 위치
    public Transform target;
    // 이동 속도
    public float speed = 4;
    // 회전 속도
    public float turnSpeed = 3;
    // 회전 거리 임계치
    public float turnDst = 5;
    // 정지 거리 임계치
    public float stoppingDst = 10;
    // 경로 따라 이동 중인지 여부
    bool followingPath = true;

    // 경로 정보
    Path path;

    // 경로 이동 정지 메소드
    public void StopMethod()
    {
        followingPath = false;
    }

    // 경로 이동 시작 메소드
    public void StartMethod()
    {
        followingPath = true;
        StartCoroutine(UpdatePath());
    }

    // 경로 요청 결과 콜백 함수
    public void OnPathFound(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            // 경로 객체 생성
            path = new Path(waypoints, transform.position, turnDst, stoppingDst);

            // 경로 이동 코루틴 중지 및 시작
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    // 경로 업데이트 코루틴
    IEnumerator UpdatePath()
    {
        if (Time.timeSinceLevelLoad < .3f)
        {
            yield return new WaitForSeconds(.3f);
        }

        // 초기 경로 요청
        PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = target.position;

        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);

            // 목표 위치가 이동한 경우에만 경로 요청
            if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
            {
                PathRequestManager.RequestPath(new PathRequest(transform.position, target.position, OnPathFound));
                targetPosOld = target.position;
            }
        }
    }

    IEnumerator FollowPath()
    {
        int pathIndex = 0;
        transform.LookAt(path.lookPoints[0]); // 첫 번째 경로 지점을 바라보도록 회전

        float speedPercent = 1;

        while (followingPath) // 경로를 따라 이동하는 동안 반복
        {
            Vector2 pos2D = new Vector2(transform.position.x, transform.position.z); // 유닛의 위치를 2D 벡터로 변환

            while (path.turnBoundaries[pathIndex].HasCrossedLine(pos2D)) // 다음 경로 지점까지 이동하는 동안 반복
            {
                if (pathIndex == path.finishLineIndex) // 경로의 마지막 지점까지 도달한 경우
                {
                    followingPath = false; // 경로 이동 중지
                    break;
                }
                else // 다음 경로 지점으로 이동
                {
                    pathIndex++;
                }
            }

            if (followingPath) // 경로 이동이 진행 중인 경우
            {
                if (pathIndex >= path.slowDownIndex && stoppingDst > 0) // 경로를 따라 이동하다가 일정 거리 이내에 도달한 경우 속도 감소
                {
                    speedPercent = Mathf.Clamp01(path.turnBoundaries[path.finishLineIndex].DistanceFromPoint(pos2D) / stoppingDst); // 속도 감소 비율 계산
                    if (speedPercent < 0.01f) // 속도가 거의 0이 되었을 경우
                    {
                        followingPath = false; // 경로 이동 중지
                    }
                }

                Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position); // 다음 경로 지점을 바라보도록 회전
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed); // 회전
                transform.Translate(Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self); // 이동
            }

            yield return null; // 다음 프레임까지 대기

        }
    }

}