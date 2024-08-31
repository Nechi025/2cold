using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    private Enemy1 _model;
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float minDistance = 0.1f;
    private int nextPoint = 0;

    private void Awake()
    {
        _model = GetComponent<Enemy1>();
    }

    private void Update()
    {
        if (GlobalPause.IsPaused()) return;

        PatrolMovement();
    }

    private void PatrolMovement()
    {
        Vector2 targetPosition = patrolPoints[nextPoint].position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, patrolSpeed * Time.deltaTime);

        // Rotar hacia el punto de patrullaje usando Quaternion.Lerp para suavizar la rotación
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < minDistance)
        {
            nextPoint = (nextPoint + 1) % patrolPoints.Length;
        }
    }
}
