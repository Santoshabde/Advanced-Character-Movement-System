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

    [Header("Ground Checks")]
    [SerializeField, Range(0,1)] private float groundCheckRadius;
    [SerializeField] private Vector3 groundCheckOffset;
    [SerializeField] private LayerMask groundCheckIgnoreLayerMask;

    private IInputSystem inputSystem;
    private Vector3 moveDirection;
    public Vector3 moveInput;
    private bool hasControl = true;

    public Vector3 MoveInput => moveInput;

    private void Awake()
    {
        inputSystem = new KeyboardAndMouseInputSystem();
    }

    private void Update()
    {
        if (!hasControl)
            return;

        if (IsCharacterGrounded())
        {
            moveInput = new Vector3(inputSystem.HorizontalX(), 0, inputSystem.VerticalX());
            if (CanCharacterMove())
            {
                MoveCharacter(moveInput);
            }
        }
        else
        {
            moveInput.x = 0;
            moveInput.z = 0;
            moveInput.y -= Time.deltaTime * Physics.gravity.magnitude * 0.01f;
            characterController.Move((transform.forward * Time.deltaTime * 3f) + moveInput);
        }
    }

    private void MoveCharacter(Vector3 moveInput)
    {
        moveDirection = cameraController.transform.rotation * moveInput;
        moveDirection.y = 0;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * characterTurnSpeed);

        Vector3 velocity = (moveDirection + new Vector3(0, -0.2f, 0)) * characterSpeed * Time.deltaTime;
        characterController.Move(velocity);
    }

    #region Private Functions
    private bool CanCharacterMove() => moveInput.magnitude > 0.1f;
    #endregion

    #region Public Functions
    public bool IsCharacterGrounded()
    {
        bool isGrounded = true;
        isGrounded = Physics.CheckSphere(transform.position + groundCheckOffset, groundCheckRadius, groundCheckIgnoreLayerMask);
        return isGrounded;
    }

    public void SetControl(bool value)
    {
        hasControl = value;
        characterController.enabled = value;
    }
    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + groundCheckOffset, groundCheckRadius);
    }

    #endregion
}
