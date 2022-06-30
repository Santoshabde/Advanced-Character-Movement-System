using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    [Header("Character Locomotion Parameters")]
    [SerializeField] private float characterSpeed;
    [SerializeField] private float characterTurnSpeed;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private CharacterCameraController cameraController;

    private IInputSystem inputSystem;
    private Vector3 moveDirection;
    private Vector3 moveInput;

    public Vector3 MoveInput => moveInput;

    private void Awake()
    {
        inputSystem = new KeyboardAndMouseInputSystem();
    }

    private void Update()
    {
        moveInput = new Vector3(inputSystem.HorizontalX(), 0, inputSystem.VerticalX());

        if (CanCharacterMove())
        {
            MoveCharacter(moveInput);
        }
    }

    private void MoveCharacter(Vector3 moveInput)
    {
        moveDirection = cameraController.transform.rotation * moveInput;
        moveDirection.y = 0;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * characterTurnSpeed);
        characterController.Move((moveDirection) * characterSpeed * Time.deltaTime);
    }

    private bool CanCharacterMove() => moveInput.magnitude > 0.1f;
}
