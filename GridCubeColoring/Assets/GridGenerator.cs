using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridGenerator : MonoBehaviour
{
    [Header("Grid")]
    public int Colums =0;
    public int Rows =0;
    public GameObject cell;
    public GameObject grid;
    [Header("Save grid")]
    public bool AplayGrid;
    private void OnValidate()
    {
        if (AplayGrid == true)
        {
            AddCells(Colums, Rows);
        }
    }
    private void AddCells(int colums, int rows)
    {
        int cells = colums * rows;
        if (AplayGrid == true) {
            DestroyChildren(grid);
            grid.GetComponent<GridLayoutGroup>().constraint = GridLayoutGroup.Constraint.FixedRowCount;
            grid.GetComponent<GridLayoutGroup>().constraintCount = Rows;
            for (int i = 0; i < cells; i++)
            {
                GameObject newCell = Instantiate(cell, new Vector3(0, 0, 0), Quaternion.identity);
                newCell.transform.parent = grid.transform;
                if( i == cells - 1)
                {
                    AplayGrid = false;
                }
            }          
        }
    }
    private void DestroyChildren(GameObject parent)
    {
        for (int i = 0; i < parent.transform.childCount; i++) 
        { 
         GameObject child = parent.transform.GetChild(i).gameObject;
            UnityEditor.EditorApplication.delayCall += () =>
            {
                GameObject.DestroyImmediate(child);
            };
        }     
    }
}
