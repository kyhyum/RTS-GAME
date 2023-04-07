using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Line
{
    // 수직선의 기울기값
    const float verticalLineGradient = 1e5f;

    // 직선의 기울기값, y절편, 직선 상의 두 점
    float gradient;
    float y_intercept;
    Vector2 pointOnLine_1;
    Vector2 pointOnLine_2;

    // 수직선과의 직교기울기값
    float gradientPerpendicular;

    // 수직선으로부터 직선이 어느쪽에 있는지를 나타내는 bool 변수
    bool approachSide;

    public Line(Vector2 pointOnLine, Vector2 pointPerpendicularToLine)
    {
        float dx = pointOnLine.x - pointPerpendicularToLine.x;
        float dy = pointOnLine.y - pointPerpendicularToLine.y;

        // 수직선의 경우 직교기울기값을 무한대로 설정한다
        if (dx == 0)
        {
            gradientPerpendicular = verticalLineGradient;
        }
        else
        {
            gradientPerpendicular = dy / dx;
        }

        // 수직선의 경우 직선의 기울기값을 무한대로 설정한다
        if (gradientPerpendicular == 0)
        {
            gradient = verticalLineGradient;
        }
        else
        {
            gradient = -1 / gradientPerpendicular;
        }

        // y절편 값 계산
        y_intercept = pointOnLine.y - gradient * pointOnLine.x;

        // 직선 상의 두 점 설정
        pointOnLine_1 = pointOnLine;
        pointOnLine_2 = pointOnLine + new Vector2(1, gradient);

        approachSide = false;
        approachSide = GetSide(pointPerpendicularToLine);
    }

    bool GetSide(Vector2 p)
    {
        // 수직선으로부터 점 P까지의 거리를 이용하여 점 P가 직선의 어느쪽에 있는지 확인
        return (p.x - pointOnLine_1.x) * (pointOnLine_2.y - pointOnLine_1.y) > (p.y - pointOnLine_1.y) * (pointOnLine_2.x - pointOnLine_1.x);
    }

    // 점 P가 직선을 넘어가는지 확인하는 함수
    public bool HasCrossedLine(Vector2 p)
    {
        return GetSide(p) != approachSide;
    }

    // 점 P와 직선의 거리를 반환하는 함수
    public float DistanceFromPoint(Vector2 p)
    {
        float yInterceptPerpendicular = p.y - gradientPerpendicular * p.x;
        float intersectX = (yInterceptPerpendicular - y_intercept) / (gradient - gradientPerpendicular);
        float intersectY = gradient * intersectX + y_intercept;
        return Vector2.Distance(p, new Vector2(intersectX, intersectY));
    }

 

}