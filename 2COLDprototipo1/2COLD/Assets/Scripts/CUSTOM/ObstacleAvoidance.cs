using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance
{
    float _angle;
    float _radius;
    Transform _entity;
    LayerMask _maskObs;

    public ObstacleAvoidance(Transform entity, float angle, float radius, LayerMask maskObs)
    {
        _angle = angle;
        _radius = radius;
        _entity = entity;
        _maskObs = maskObs;
    }

    public Vector2 GetDir(Vector2 currentDir)
    {
        Collider2D[] colls = Physics2D.OverlapCircleAll(_entity.position, _radius, _maskObs);
        Collider2D nearColl = null;
        Vector2 closetPoint = Vector2.zero;
        float nearCollDistance = 0;

        for (int i = 0; i < colls.Length; i++)
        {
            var currentColl = colls[i];
            closetPoint = currentColl.ClosestPoint(_entity.position);
            Vector2 dirToColl = closetPoint - (Vector2)_entity.position;
            float currentAngle = Vector2.Angle(dirToColl, currentDir);
            float distance = dirToColl.magnitude;

            if (currentAngle > _angle / 2) { continue; }

            if (nearColl == null)
            {
                nearColl = currentColl;
                nearCollDistance = distance;
                continue;
            }

            if (distance < nearCollDistance)
            {
                nearCollDistance = distance;
                nearColl = currentColl;
            }
        }

        if (nearColl == null)
        {
            return currentDir;
        }
        else
        {
            Vector2 relativePos = _entity.InverseTransformPoint(closetPoint);
            Vector2 dirToClosetPoint = (closetPoint - (Vector2)_entity.position).normalized;
            Vector2 newDir;
            if (relativePos.x < 0)
            {
                newDir = Vector2.Perpendicular(dirToClosetPoint);
            }
            else
            {
                newDir = -Vector2.Perpendicular(dirToClosetPoint);
            }
            return Vector2.Lerp(currentDir, newDir, (_radius - Mathf.Clamp(nearCollDistance - 1, 0, _radius)) / _radius);
        }
    }
}
