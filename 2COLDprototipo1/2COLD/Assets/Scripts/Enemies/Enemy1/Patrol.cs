using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    Enemy1 _model;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float minDistance;
    private int nextPoint = 0;

    private void Awake()
    {
        _model = GetComponent<Enemy1>();
    }

    private void Update()
    {
        if (!_model.moveToPlayer)
        {
            if (GlobalPause.IsPaused())
                return;

            Vector2 targetPosition = patrolPoints[nextPoint].position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, patrolSpeed * Time.deltaTime);

            _model.LookDir(targetPosition, transform.position);

            if (Vector2.Distance(transform.position, targetPosition) < minDistance)
            {
                nextPoint++;
                if (nextPoint >= patrolPoints.Length)
                {
                    nextPoint = 0;
                }
            }
        }
    }
}
