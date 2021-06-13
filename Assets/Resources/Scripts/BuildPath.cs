using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum turnSide
{
    Horizontal, Vertical/*, LeftUp, LeftDown, RightUp, RightDown*/
}

public class BuildPath : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private List<Transform> path = new List<Transform>();
    public List<Transform> Path { get => path;}

    [SerializeField] private GameObject[] tile;

    [SerializeField] private Transform previousCell;
    [SerializeField] private Transform currentCell;
    private GameObject turnTile;

    private CheckNearestCells checkNearestCells;

    public turnSide turn;

    public GameObject blinkEffect;

    private Transform tempCell;

    private void Start()
    {
        checkNearestCells = GetComponent<CheckNearestCells>();
        blinkEffect.SetActive(false);
    }

    void Update()
    {
        GetPath();
    }
     
    /// <summary>
    /// Строим путь корабля
    /// </summary>
    private void GetPath()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray;
            ray = cam.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                if (currentCell != null)
                {
                    blinkEffect.SetActive(true);
                    blinkEffect.transform.position = currentCell.position;
                    if (currentCell.transform.position != hit.collider.transform.position)
                    {
                        print(CanMove(hit));
                        previousCell = currentCell;

                        if (CanMove(hit))
                        {
                            CheckTurn(hit);
                            AddCellInPath(hit);
                        }
                    }
                    GetNotDiagonalCell(currentCell, hit);
                }
                else if (currentCell == null)
                {
                    GetNotDiagonalCell(hit.transform, hit);
                }
            }
        }
    }

    /// <summary>
    /// Добавляем ячейку в список ячеек, из которых состоит путь
    /// </summary>
    /// <param name="hit">инфо о пересечении луча</param>
    private void AddCellInPath(RaycastHit hit)
    {
        path.Add(hit.collider.transform);

        switch (turn)
        {
            case turnSide.Horizontal:
                turnTile = Instantiate(tile[0], previousCell);
                break;
            case turnSide.Vertical:
                turnTile = Instantiate(tile[1], previousCell);
                break;
        }

        turnTile.transform.parent = previousCell;
        checkNearestCells.FindNearestCells(hit.collider.transform);
    }

    /// <summary>
    /// Узнаем, с какой стороны очередная ячейка для правильной
    /// расстановка тайлов поворота
    /// </summary>
    private void CheckTurn(RaycastHit hit)
    {
        if (previousCell != null)
        {
            if (currentCell.position.x == hit.collider.transform.position.x)
            {
                turn = turnSide.Horizontal;
            }
            else if (currentCell.position.z == hit.collider.transform.position.z)
            {
                turn = turnSide.Vertical;
            }
        }
    }

    /// <summary>
    /// Возможен ли ход
    /// </summary>
    /// <param name="hit">луч</param>
    /// <returns></returns>
    private bool CanMove(RaycastHit hit)
    {
        if (Vector3.Distance(currentCell.position, hit.collider.transform.position) == checkNearestCells.MinDistanse
            && hit.collider.TryGetComponent(out Cell cell))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Получаем ближайшую ячейку не по диагонали
    /// </summary>
    /// <param name="trns">ячейка</param>
    /// <param name="hit">луч</param>
    private void GetNotDiagonalCell(Transform trns, RaycastHit hit)
    {
        for (int i = 0; i < checkNearestCells.NearestCells.Count; i++)
        {
            if (Vector3.Distance(trns.position, checkNearestCells.NearestCells[i].position) < checkNearestCells.MinDistanse)
            {
                currentCell = hit.collider.transform;
            }
        }
    }
}
