using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private int x;
    [SerializeField] private int y;

    public int x_coord => x;
    public int y_coord => y;

    public void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
