using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private InputAction moveInput;
    [SerializeField] private InputAction runInput;
    [SerializeField] private InputAction jumpInput;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float jumpForce = 5;
    [SerializeField] private float jumpCooldown = 0.5f;
    [SerializeField] private PlayerAnimation anim;
    [SerializeField] private Rigidbody rgb;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Transform cameraDir;

    private Vector2 inputVector;
    private Vector3 moveVector;
    private bool isJumping;
    private float startTimer;

    private void Start()
    {
        moveInput.Enable();
        runInput.Enable();
        jumpInput.Enable();
    }

    private void Update()
    {
        inputVector = moveInput.ReadValue<Vector2>();
        Vector3 cameraForward = cameraDir.forward; //old, here just in case: moveVector = new Vector3(inputVector.x, 0, inputVector.y) * moveSpeed * Time.deltaTime;
        Vector3 cameraRight = cameraDir.right;
        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        moveVector = (cameraForward * inputVector.y + cameraRight * inputVector.x) * moveSpeed * Time.deltaTime;
        transform.localPosition += moveVector;

        if (inputVector.magnitude > 0)
        {
            transform.forward = moveVector.normalized;
            switch (isJumping, runInput.ReadValue<float>())
            {
                case (false, > 0):
                    transform.position += moveVector * 0.5f;
                    anim.SetAnimState(PlayerAnimState.Run);
                    break;
                case (false, 0):
                    anim.SetAnimState(PlayerAnimState.Walk);
                    break;
                case (true, > 0):
                case (true, 0):
                    break;
            }
        }
        else
        {
            if (rgb.velocity.y==0)
            {
                anim.SetAnimState(PlayerAnimState.Idle); 
            }

        }

        isJumping = !Physics.Raycast(transform.position, Vector3.down, 0.1f, layerMask);
        if (jumpInput.ReadValue<float>() > 0 && !isJumping && Time.time > startTimer + jumpCooldown) 
        {
            rgb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetAnimState(PlayerAnimState.Jump);
            startTimer = Time.time;
        }
        
    }
}
