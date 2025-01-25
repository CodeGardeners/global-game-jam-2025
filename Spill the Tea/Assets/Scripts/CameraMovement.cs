using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.005f;

    private InputAction move;
    private InputAction moveUpDown;

    public void Start()
    {
        move = Inputs.Instance.Actions.Player.MoveCamera;
        moveUpDown = Inputs.Instance.Actions.Player.MoveCameraUpDown;
    }

    public void Update()
    {
        MoveCamera();
        MoveCameraUpDown();
    }

    private void MoveCamera()
    {
        if (!move.IsPressed())
        {
            return;
        }
        var value = move.ReadValue<Vector2>();
        var pos = transform.position;
        pos.x += value.x * speed;
        pos.z += value.y * speed;
        transform.position = pos;
    }

    private void MoveCameraUpDown()
    {
        if (!moveUpDown.IsPressed())
        {
            return;
        }
        var value = moveUpDown.ReadValue<Vector2>();
        var pos = transform.position;
        pos.y += value.y * speed;
        transform.position = pos;
    }
}
