using System;
using UnityEngine;

[Serializable]
public class Grid
{
    public string gridName;
    public int rows;
    public int cols;
}

public class GridData : MonoBehaviour 
{
    public Grid grid;
}