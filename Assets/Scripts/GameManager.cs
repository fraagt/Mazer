using System;
using Signals;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;

    public GameObject MazeBox;

    private Maze _maze;

    public int mazeHeight;
    public int mazeWidth;

    private PlayerMazePosition _playerMazePosition;

    void Start()
    {
        SignalBus.Instance.Subscribe<SignalPlayerMove>(OnPlayerMove);

        _maze = MazeUtils.GenerateMaze(mazeHeight, mazeWidth);
        
        for (int z = 0; z < _maze.Height; z++)
        {
            for (int x = 0; x < _maze.Width; x++)
            {
                Box box =
                    Instantiate(MazeBox, new Vector3(x * 3, 0, z * 3), Quaternion.identity, transform).GetComponent<Box>();

                box.Ground.SetActive(_maze.Cells[z, x].Ground);
                box.Girder.SetActive(_maze.Cells[z, x].Girder);
                box.WallDown.SetActive(_maze.Cells[z, x].WallDown);
                box.WallLeft.SetActive(_maze.Cells[z, x].WallLeft);
            }
        }

        var playerObj = Instantiate(player, new Vector3(_maze.Cells[0, 0].X + 1.5f, 1.5f, _maze.Cells[0, 0].Z + 1.5f),
            Quaternion.identity);

        _playerMazePosition = new PlayerMazePosition
        {
            PlayerController = playerObj,
            CurrentCell = _maze.Cells[0, 0]
        };
    }

    private void OnDestroy()
    {
        SignalBus.Instance.Unsubscribe<SignalPlayerMove>(OnPlayerMove);
    }

    private void OnPlayerMove(SignalPlayerMove signal)
    {
        var currCell = _playerMazePosition.CurrentCell;

        var nextZ = currCell.Z + (int) signal.Direction.y;
        var nextX = currCell.X + (int) signal.Direction.x;

        if (nextZ < 0 || nextZ >= _maze.Cells.GetLength(0) ||
            nextX < 0 || nextX >= _maze.Cells.GetLength(1))
            return;
        
        var nextCell = _maze.Cells[nextZ, nextX];

        if (MazeUtils.IsThereWallBetweenCells(currCell, nextCell))
            return;

        _playerMazePosition.CurrentCell = nextCell;
        
        var transform = _playerMazePosition.PlayerController.transform;

        transform.position = new Vector3(nextCell.X * 3f + 1.5f, transform.position.y, nextCell.Z * 3f + 1.5f);
    }
}
