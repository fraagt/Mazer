using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Signals;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            Move(Vector2.up);
        if (Input.GetKeyDown(KeyCode.S))
            Move(Vector2.down);
        if (Input.GetKeyDown(KeyCode.D))
            Move(Vector2.right);
        if (Input.GetKeyDown(KeyCode.A))
            Move(Vector2.left);
    }

    private void Move(Vector2 direction)
    {
        SignalBus.Instance.Fire(new SignalPlayerMove(direction.normalized));
    }
}
