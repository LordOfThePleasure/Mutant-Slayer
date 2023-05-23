using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentDetector : MonoBehaviour
{
    [SerializeField] private LayerMask opponentLayerMask;


    public Transform FindTarget(float distance)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, distance, opponentLayerMask);

        if (colliders.Length == 0)
        {
            return null;
        }

        Collider2D closestEnemy = colliders[0];
        float closestDistance = Vector2.Distance(closestEnemy.transform.position, transform.position);

        foreach (var enemy in colliders)
        {
            float distanceToEnemy = Vector2.Distance(closestEnemy.transform.position, transform.position);

            if (distanceToEnemy < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distanceToEnemy;
            }
        }

        return closestEnemy.transform;
    }
}
