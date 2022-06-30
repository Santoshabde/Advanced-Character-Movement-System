using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCameraController : MonoBehaviour
{
    [SerializeField] private GameObject toFollow;
    [SerializeField] private Transform toLookAt;

    [Header("Camera Properties")]
    [SerializeField] private float cameraDistance;
    [SerializeField] private float mouseXSensivity;
    [SerializeField] private float mouseYSensivity;
    [SerializeField] private float minVerticalAngle;
    [SerializeField] private float maxVerticalAngle;
    [SerializeField] private bool invertedAxis;
    [SerializeField] private bool cursorVisibility;
    [SerializeField] private Vector2 lookAtOffset;

    private IInputSystem inputSystem;

    private float rotationX;
    private float rotationY;
    private void Awake()
    {
        inputSystem = new KeyboardAndMouseInputSystem();
        Cursor.visible = cursorVisibility;

        if (toLookAt == null)
            toLookAt = toFollow.transform;
    }

    private void Update()
    {
        int isAxisInverted = invertedAxis ? -1 : 1;

        rotationX += isAxisInverted * inputSystem.MouseX() * mouseXSensivity;

        rotationY += isAxisInverted * inputSystem.MouseY() * mouseYSensivity;
        rotationY = Clamp(rotationY, minVerticalAngle, maxVerticalAngle);

        var toRotatet = Quaternion.Euler(0, rotationX, rotationY);
        Vector3 offSet = toRotatet * new Vector3(cameraDistance, 0, 0);

        transform.position = toFollow.transform.position + offSet + new Vector3(lookAtOffset.x, lookAtOffset.y);
        transform.LookAt(toLookAt);
    }

    private float Clamp(float value, float min, float max)
    {
        if (value > max)
            value = max;

        if (value < min)
            value = min;

        return value;
    }
}
