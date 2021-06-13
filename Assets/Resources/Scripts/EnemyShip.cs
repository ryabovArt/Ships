using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : ShipsBehaviour
{
     [SerializeField] private Animator enemyAnimator;

    void Update()
    {
        Move();
    }

    /// <summary>
    /// Смена индекса точки на пути корабля
    /// </summary>
    public override void ChangeCurrentPathIndex()
    {
        if (Vector3.Distance(transform.position, path[currentPathIndex].position) == 0f)
        {
            currentPathIndex++;
            if (currentPathIndex >= 4)
            {
                currentPathIndex = 0;
            }
        }
    }

    /// <summary>
    /// Анимация движения корабля
    /// </summary>
    public void EnemyMoveAnimation()
    {
        enemyAnimator.SetTrigger("Move");
    }
}
