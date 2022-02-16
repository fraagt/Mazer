using System;

[Serializable]
public class MazeCell
{
    public int X;
    public int Z;

    public bool Ground = true;
    public bool Girder = true;
    public bool WallDown = true;
    public bool WallLeft = true;

    public bool Visited = false;

    public uint DistanceFromStart;
}
