using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPoint : MonoBehaviour
{
    public Transform anchor;
    public Transform startAngle;
    public Transform endAngle;
    public int id;

    public void SetAngleBasedOnPoint(ControlPoint pointBefore)
    {
        Vector3 direction = (pointBefore.anchor.position - anchor.position).normalized;
        startAngle.position = anchor.position;
        startAngle.position += direction * 2;
        endAngle.position = anchor.position;
        endAngle.position -= direction * 2;
    }

    public void SetAnglesFromAverage(ControlPoint pointBefore, ControlPoint pointAfter)
    {
        Vector3 dirBefore = (pointBefore.anchor.position - anchor.position).normalized;
        Vector3 dirAfter = (pointAfter.anchor.position - anchor.position).normalized;

        Vector3 direction = (dirAfter - dirBefore).normalized;

        startAngle.position = anchor.position;
        startAngle.position += direction * 2;
        endAngle.position = anchor.position;
        endAngle.position -= direction * 2;
    }
}
