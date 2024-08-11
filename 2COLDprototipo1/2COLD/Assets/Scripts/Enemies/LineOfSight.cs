using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class LineOfSight : MonoBehaviour
{
    public float range;
    [Range(1, 360)]
    public float angle;
    public LayerMask maskObs;

    public bool CheckRange(Transform target)
    {
        return CheckRange(target, range);
    }

    public bool CheckRange(Transform target, float range)
    {
        float distance = Vector2.Distance(target.position, Origin);
        return distance <= range;
    }

    public bool CheckAngle(Transform target)
    {
        return CheckAngle(target, angle);
    }

    public bool CheckAngle(Transform target, float angle)
    {
        Vector2 dirToTarget = (Vector2)target.position - Origin;  // Conversión explícita a Vector2
        float angleToTarget = Vector2.Angle(Forward, dirToTarget);
        return angleToTarget <= angle / 2;
    }

    public bool CheckView(Transform target)
    {
        return CheckView(target, maskObs);
    }

    public bool CheckView(Transform target, LayerMask maskObs)
    {
        Vector2 dirToTarget = (Vector2)target.position - Origin;  // Conversión explícita a Vector2
        float distance = dirToTarget.magnitude;
        return !Physics2D.Raycast(Origin, dirToTarget, distance, maskObs);
    }

    Vector2 Origin => transform.position;
    Vector2 Forward => transform.up; // Usamos `up` en lugar de `forward` en 2D

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(Origin, range);

        Vector3 rightBoundary = Quaternion.Euler(0, 0, angle / 2) * Forward * range;
        Vector3 leftBoundary = Quaternion.Euler(0, 0, -angle / 2) * Forward * range;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(Origin, rightBoundary);
        Gizmos.DrawRay(Origin, leftBoundary);
    }
}
