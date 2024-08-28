using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggressiveEnemy : EnemyNavigation
{
    private readonly float safeDistance = 3.0f;
    private void Update()
    {
        TrackPlayer();
        NextMove();
    }

    private void TrackPlayer()
    {
        Debug.Log($"State:{state}");
        Debug.Log($"starts tracking");
        float distance = Vector2.Distance(player.position, transform.position);
        if (distance < safeDistance)
        {
            state = EnemyStates.CHASE;
            Debug.Log($"State:{state}");
            ;
        }
    }
}
