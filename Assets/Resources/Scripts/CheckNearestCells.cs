using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckNearestCells : MonoBehaviour
{
    [SerializeField] private Transform startCell;
    [SerializeField] private List<Transform> cell = new List<Transform>();
    [SerializeField] private List<Transform> nearestCells = new List<Transform>();
    public List<Transform> NearestCells { get => nearestCells; }

    [SerializeField] private Transform verticalCell;

    private float minDistance;
    public float MinDistanse { get => minDistance; }

    public bool canMove;

    void Start()
    {
        FindAllCells();

        CheckMinAndMaxDistanceBetweenCells();

        FindNearestCells(startCell);
    }

    /// <summary>
    /// Ќаходим все €чейки
    /// </summary>
    private void FindAllCells()
    {
        foreach (Transform child in gameObject.GetComponentInChildren<Transform>())
        {
            cell.Add(child);
        }
    }

    /// <summary>
    /// ќпредел€ем минимальную и максимальную дистанцию между ближайшими от стартовой €чейками
    /// </summary>
    private void CheckMinAndMaxDistanceBetweenCells()
    {
        minDistance = Vector3.Distance(verticalCell.transform.position, startCell.transform.position);
    }

    /// <summary>
    /// »щем ближайшие €чейки
    /// </summary>
    public void FindNearestCells(Transform startCell)
    {
        nearestCells.Clear();

        for (int i = 0; i < cell.Count; i++)
        {
            if (Vector3.Distance(startCell.position, cell[i].position) > 1.4f )
            {
                canMove = false;
            }
            if (startCell.position.x == cell[i].position.x)
            {
                if (startCell.position.z < cell[i].position.z && (int)cell[i].position.z == (int)startCell.position.z + (int)minDistance)
                {
                    nearestCells.Add(cell[i]);
                }
                if (startCell.position.z > cell[i].position.z && (int)cell[i].position.z == ((int)startCell.position.z - (int)minDistance))
                {
                    nearestCells.Add(cell[i]);
                }
            }

            if (startCell.position.z == cell[i].position.z)
            {
                if (startCell.position.x > cell[i].position.x && (int)cell[i].position.x == ((int)startCell.position.x - (int)minDistance))
                {
                    nearestCells.Add(cell[i]);
                }
                if (startCell.position.x < cell[i].position.x && (int)cell[i].position.x == ((int)startCell.position.x + (int)minDistance))
                {
                    nearestCells.Add(cell[i]);
                }
            }
        }
    }
}
