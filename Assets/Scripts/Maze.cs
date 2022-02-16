using System;
using UnityEngine;

[Serializable]
public class Maze
{
    public Vector3 Origin;
    public int Height;
    public int Width;
    public MazeCell[,] Cells;
    public MazeCell Exit;
}
