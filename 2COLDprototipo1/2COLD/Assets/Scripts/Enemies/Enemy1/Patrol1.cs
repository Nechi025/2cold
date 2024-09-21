using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol1 : MonoBehaviour
{
    
    Enemy2 _model2;
    [SerializeField] private float patrolSpeed;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float minDistance;
    private int nextPoint = 0;

    private void Awake()
    {
        
        _model2 = GetComponent<Enemy2>();
    }

    private void Update()
    {
        if (!_model2.moveToPlayer)
        {
            if (GlobalPause.IsPaused())
                return;

            Vector2 targetPosition = patrolPoints[nextPoint].position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, patrolSpeed * Time.deltaTime);

            _model2.LookDir(targetPosition, transform.position);

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