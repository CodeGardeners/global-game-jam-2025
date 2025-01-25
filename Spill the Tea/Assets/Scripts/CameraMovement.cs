using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public void OnEnable()
    {
        Inputs.Instance.Actions.Player.MoveCamera.performed += MoveCamera;
        Inputs.Instance.Actions.Player.MoveCameraUpDown.performed += MoveCameraUpDown;
    }
    public void OnDisable()
    {
        Inputs.Instance.Actions.Player.MoveCamera.performed -= MoveCamera;
        Inputs.Instance.Actions.Player.MoveCameraUpDown.performed -= MoveCameraUpDown;
    }

    private void MoveCamera(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        Debug.Log(value);
    }

    private void MoveCameraUpDown(InputAction.CallbackContext context)
    {
        var value = context.ReadValue<Vector2>();
        Debug.Log(value);
    }
}
