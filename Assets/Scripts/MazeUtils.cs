using System.Collections.Generic;
using UnityEngine;

public static class MazeUtils
{
    public static Maze GenerateMaze(int height, int width)
    {
        Maze maze = new Maze
        {
            Origin = Vector3.zero,
            Height = height,
            Width = width,
            Cells = new MazeCell[height, width]
        };

        for (int z = 0; z < maze.Cells.GetLength(0); z++)
        {
            for (int x = 0; x < maze.Cells.GetLength(1); x++)
            {
                maze.Cells[z, x] = new MazeCell {Z = z, X = x};
            }
        }

        for (int z = 0; z < maze.Cells.GetLength(0); z++)
        {
            maze.Cells[z, width - 1].Ground = false;
            maze.Cells[z, width - 1].WallDown = false;
        }

        for (int x = 0; x < maze.Cells.GetLength(1); x++)
        {
            maze.Cells[height - 1, x].Ground = false;
            maze.Cells[height - 1, x].WallLeft = false;
        }

        RemoveWallsWithBacktracker(maze.Cells);

        maze.Exit = PlaceMazeExit(maze.Cells);

        return maze;
    }

    private static void RemoveWallsWithBacktracker(MazeCell[,] maze)
    {
        MazeCell current = maze[0, 0];
        current.Visited = true;
        current.DistanceFromStart = 0;

        Stack<MazeCell> stack = new Stack<MazeCell>();
        do
        {
            List<MazeCell> unvisitedNeighbours = new List<MazeCell>();

            int cZ = current.Z;
            int cX = current.X;

            if (cZ > 0 && !maze[cZ - 1, cX].Visited)
                unvisitedNeighbours.Add(maze[cZ - 1, cX]);
            if (cX > 0 && !maze[cZ, cX - 1].Visited)
                unvisitedNeighbours.Add(maze[cZ, cX - 1]);
            if (cZ < maze.GetLength(0) - 2 && !maze[cZ + 1, cX].Visited)
                unvisitedNeighbours.Add(maze[cZ + 1, cX]);
            if (cX < maze.GetLength(1) - 2 && !maze[cZ, cX + 1].Visited)
                unvisitedNeighbours.Add(maze[cZ, cX + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeCell chosen =
                    unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];

                RemoveWall(current, chosen);

                chosen.Visited = true;
                stack.Push(chosen);
                current = chosen;
                chosen.DistanceFromStart = (uint) stack.Count;
            }
            else
            {
                current = stack.Pop();
            }
        } while (stack.Count > 0);
    }

    private static void RemoveWall(MazeCell a, MazeCell b)
    {
        if (a.Z == b.Z)
        {
            if (a.X > b.X) a.WallLeft = false;
            else b.WallLeft = false;
        }
        else
        {
            if (a.Z > b.Z) a.WallDown = false;
            else b.WallDown = false;
        }
    }

    private static MazeCell PlaceMazeExit(MazeCell[,] maze)
    {
        MazeCell furthest = maze[0, 0];

        for (int z = 0; z < maze.GetLength(0); z++)
        {
            if (maze[z, maze.GetLength(1) - 2].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[z, maze.GetLength(1) - 2];
            if (maze[z, 0].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[z, 0];
        }

        for (int x = 0; x < maze.GetLength(1); x++)
        {
            if (maze[maze.GetLength(0) - 2, x].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[maze.GetLength(1) - 2, x];
            if (maze[0, x].DistanceFromStart > furthest.DistanceFromStart)
                furthest = maze[0, x];
        }

        if (furthest.X == 0)
            furthest.WallLeft = false;
        else if (furthest.Z == 0)
            furthest.WallDown = false;
        else if (furthest.X == maze.GetLength(1) - 2)
            maze[furthest.Z, furthest.X + 1].WallLeft = false;
        else if (furthest.Z == maze.GetLength(0) - 2)
            maze[furthest.Z + 1, furthest.X].WallDown = false;

        return furthest;
    }

    public static bool IsThereWallBetweenCells(MazeCell a, MazeCell b)
    {
        if (Mathf.Abs(a.X - b.X) > 1f ||
            Mathf.Abs(a.Z - b.Z) > 1f)
            return true;

        if (a.Z > b.Z)
            return a.WallDown;
        if (a.Z < b.Z)
            return b.WallDown;
        if (a.X > b.X)
            return a.WallLeft;
        if (a.X < b.X)
            return b.WallLeft;

        return true;
    }
}
