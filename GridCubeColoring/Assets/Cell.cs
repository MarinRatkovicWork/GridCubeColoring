using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public bool ChangeColor = false;   

    void Update()
    {
        if (ChangeColor)
        {
            this.gameObject.GetComponent<Image>().color = Color.red;
        }
    }
}
