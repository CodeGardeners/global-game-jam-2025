using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    private const float speed = 0.01f;
    [SerializeField] private float heightMin = 1.7f;
    [SerializeField] private float heightMax = 5.0f;

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

    public float GetCameraHeight()
    {
        return (transform.position.y - heightMin) / (heightMax - heightMin);
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
        pos.x = Math.Clamp(pos.x, -4f, 4.5f);
        pos.z += value.y * speed;
        pos.z = Math.Clamp(pos.z, -5f, 3f);
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
        pos.y = Math.Clamp(pos.y, heightMin, heightMax);
        transform.position = pos;
    }
}
