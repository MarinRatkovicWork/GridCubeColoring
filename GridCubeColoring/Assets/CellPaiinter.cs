using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class CellPaiinter : MonoBehaviour
{ 
    [Header("RandomAutomaticFinder")]
    [Header("Finds random closes cell and then from teth cell faind anather and so on")]
    public bool activateRandomAutomaticFinder = false;
    [Header("RandomGroupFinder")]
    [Header("Finds random closes cell change color thill all celss close to start change color then choses one of cells and continues same process.")]
    public bool activateRandomGroupFinder = false;
    [Header("RandomStartAndCloserFinder")]
    [Header("Finds random closes cell change color when no more cells are found expands serarc to find closest emty fild thil all are full")]
    public bool activateRandomStartAndCloserFinder = false;
    private CircleCollider2D _startCollider;
    private CircleCollider2D _nextCollider;
    private void Awake()
    {
        _startCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        StartAutommaticFinder();
        StartGroupFinder();
        StartRandomStartAndCloserFinder();
    }
    //RandomAutomaticFinder
    public void StartAutommaticFinder()
    {
        if (activateRandomAutomaticFinder)
        {
            if (_nextCollider != null)
            {
                RandomAutomaticFinder(_nextCollider);
            }
            else
            {
                RandomAutomaticFinder(_startCollider);
            }
        }
    }
    public void RandomAutomaticFinder(CircleCollider2D collaider)
    {                   
            Collider2D[] overlaps = Physics2D.OverlapCircleAll(collaider.bounds.center, collaider.radius);
            int count = 0;
            for (int i = 0; i < overlaps.Length; i++)
            {
                if (overlaps[i].GetType() == typeof(CircleCollider2D) && overlaps[i].GetComponent<Cell>().ChangeColor == false)
                {
                    count++;
                }
            }

            if (count > 0)
            {
                int randomIndex = Random.Range(0, count);
                int currentIndex = 0;
                for (int i = 0; i < overlaps.Length; i++)
                {
                    if (overlaps[i].GetType() == typeof(CircleCollider2D) && overlaps[i].GetComponent<Cell>().ChangeColor == false)
                    {
                        if (currentIndex == randomIndex)
                        {
                            CircleCollider2D randomOverlap = (CircleCollider2D)overlaps[i];
                            randomOverlap.gameObject.GetComponent<Cell>().ChangeColor = true;
                            Debug.Log("Number of free to paind cells: " + count.ToString());
                            _nextCollider = randomOverlap;
                            activateRandomAutomaticFinder = false;
                            break;
                        }
                        currentIndex++;
                    }
                }
            }            
        }
    //RandomGroupFinder
    public void StartGroupFinder()
    {
        if (activateRandomGroupFinder)
        {
            if (_nextCollider != null)
            {
                RandomGroupFinder(_nextCollider);
            }
            else
            {
                RandomGroupFinder(_startCollider);
            }
        }
    }
    public void RandomGroupFinder(CircleCollider2D collaider)
    {                   
            Collider2D[] overlaps = Physics2D.OverlapCircleAll(collaider.bounds.center, collaider.radius);
            int count = 0;
            for (int i = 0; i < overlaps.Length; i++)
            {
                if (overlaps[i].GetType() == typeof(CircleCollider2D) && overlaps[i].GetComponent<Cell>().ChangeColor == false)
                {
                    count++;
                }
            }

            if (count > 0)
            {
                int randomIndex = Random.Range(0, count);
                int currentIndex = 0;
                for (int i = 0; i < overlaps.Length; i++)
                {
                    if (overlaps[i].GetType() == typeof(CircleCollider2D) && overlaps[i].GetComponent<Cell>().ChangeColor == false)
                    {
                        if (currentIndex == randomIndex)
                        {
                            CircleCollider2D randomOverlap = (CircleCollider2D)overlaps[i];
                            randomOverlap.gameObject.GetComponent<Cell>().ChangeColor = true;
                            Debug.Log("Number of free to paind cells: "+ count.ToString());
                            if (count == 1)
                            {
                              _nextCollider = randomOverlap;
                            }
                     
                        activateRandomGroupFinder = false;
                        break;
                        }
                        currentIndex++;
                    }
                }
            }            
        }
    //RandomStartAndCloserFinder
    public void StartRandomStartAndCloserFinder()
    {
        if (activateRandomStartAndCloserFinder)
        {
            if (_nextCollider != null)
            {
                RandomStartAndCloserFinder(_nextCollider);
            }
            else
            {
                RandomStartAndCloserFinder(_startCollider);
            }
        }
    }
    public void RandomStartAndCloserFinder(CircleCollider2D collaider)
    {
        Collider2D[] overlaps = Physics2D.OverlapCircleAll(collaider.bounds.center, collaider.radius);
        int count = 0;
        for (int i = 0; i < overlaps.Length; i++)
        {
            if (overlaps[i].GetType() == typeof(CircleCollider2D) && overlaps[i].GetComponent<Cell>().ChangeColor == false)
            {
                count++;
            }
        }
        if(count == 0)
        {
            Debug.Log("No free to paind cells");
            Debug.Log("Expanding Colader to fid first empty");
            ExpandSearch(collaider);
        }
        if (count > 0)
        {
            int randomIndex = Random.Range(0, count);
            int currentIndex = 0;
            for (int i = 0; i < overlaps.Length; i++)
            {
                if (overlaps[i].GetType() == typeof(CircleCollider2D) && overlaps[i].GetComponent<Cell>().ChangeColor == false)
                {
                    if (currentIndex == randomIndex)
                    {
                        CircleCollider2D randomOverlap = (CircleCollider2D)overlaps[i];
                        randomOverlap.gameObject.GetComponent<Cell>().ChangeColor = true;
                        Debug.Log("Number of free to paind cells: " + count.ToString());
                        _nextCollider = randomOverlap;
                        activateRandomStartAndCloserFinder = false;
                        break;
                    }
                    currentIndex++;
                }
            }
        }
    }
    public void ExpandSearch(CircleCollider2D collaider)
    {
        collaider.radius = collaider.radius* 2;
        RandomStartAndCloserFinder(collaider);
    }
    
}
