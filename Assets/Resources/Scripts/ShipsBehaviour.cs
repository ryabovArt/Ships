using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipsBehaviour : MonoBehaviour
{
    protected int currentPathIndex;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    public List<Transform> path;

    /// <summary>
    /// Двожение кораблей
    /// </summary>
    public void Move()
    {
        if (currentPathIndex < path.Count)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[currentPathIndex].position, moveSpeed * Time.deltaTime);

            Vector3 direction = (path[currentPathIndex].position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);

            ChangeCurrentPathIndex();
        }
    }

    /// <summary>
    /// Смена индекса точки на пути корабля
    /// </summary>
    public virtual void ChangeCurrentPathIndex()
    {
        if (Vector3.Distance(transform.position, path[currentPathIndex].position) < 0.1f)
        {
            currentPathIndex++;
        }
    }
}
