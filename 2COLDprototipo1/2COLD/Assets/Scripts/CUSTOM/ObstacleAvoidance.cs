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
        Vector2 closestPoint = Vector2.zero;
        float nearCollDistance = float.MaxValue;

        foreach (Collider2D coll in colls)
        {
            Vector2 point = coll.ClosestPoint(_entity.position);
            Vector2 dirToColl = point - (Vector2)_entity.position;
            float angleToColl = Vector2.Angle(currentDir, dirToColl);
            float distance = dirToColl.magnitude;

            if (angleToColl > _angle / 2) continue;

            if (distance < nearCollDistance)
            {
                nearCollDistance = distance;
                nearColl = coll;
                closestPoint = point;
            }
        }

        if (nearColl == null)
        {
            // Si no hay colisiones cercanas, sigue en la dirección actual
            return currentDir;
        }

        // Realizamos raycasts hacia la izquierda y derecha para determinar qué lado está más despejado
        Vector2 dirToClosestPoint = (closestPoint - (Vector2)_entity.position).normalized;
        Vector2 perpendicularRight = Vector2.Perpendicular(dirToClosestPoint);
        Vector2 perpendicularLeft = -Vector2.Perpendicular(dirToClosestPoint);

        float rightDistance = CheckSideClearance(perpendicularRight);
        float leftDistance = CheckSideClearance(perpendicularLeft);

        // Elegimos el lado que tenga la mayor distancia despejada
        Vector2 newDir = rightDistance > leftDistance ? perpendicularRight : perpendicularLeft;

        return Vector2.Lerp(currentDir, newDir, (_radius - Mathf.Clamp(nearCollDistance - 1, 0, _radius)) / _radius);
    }

    private float CheckSideClearance(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(_entity.position, direction, _radius, _maskObs);
        return hit.collider ? hit.distance : _radius; // Si no hay colisión, retorna el radio completo como distancia despejada
    }

    // Método para dibujar Gizmos que representan los raycasts laterales
    public void DrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector2 dirToClosestPoint = _entity.right; // Suponiendo que la entidad esté mirando hacia la derecha
        Vector2 perpendicularRight = Vector2.Perpendicular(dirToClosestPoint);
        Vector2 perpendicularLeft = -Vector2.Perpendicular(dirToClosestPoint);

        Gizmos.DrawLine(_entity.position, (Vector2)_entity.position + perpendicularRight * _radius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_entity.position, (Vector2)_entity.position + perpendicularLeft * _radius);
    }
}
