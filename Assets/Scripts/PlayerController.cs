using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Signals;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            Move(Vector2.up);
        if (Input.GetKey(KeyCode.S))
            Move(Vector2.down);
        if (Input.GetKey(KeyCode.D))
            Move(Vector2.right);
        if (Input.GetKey(KeyCode.A))
            Move(Vector2.left);
    }

    private void Move(Vector2 direction)
    {
        SignalBus.Instance.Fire(new SignalPlayerMove(direction.normalized));
    }
}
