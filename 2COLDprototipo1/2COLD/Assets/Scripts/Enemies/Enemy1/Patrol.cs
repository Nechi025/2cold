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
        if (_model.moveToPlayer == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[nextPoint].position, patrolSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, patrolPoints[nextPoint].position) < minDistance)
            {
                nextPoint += 1;
                if (nextPoint >= patrolPoints.Length)
                {
                    nextPoint = 0;
                }
            }
        }
        
    }
}
