using System;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class PlayerController : MonoBehaviour
{
    [Header("Properties")]
    private CharacterController controller;
    private Vector2 moveInput;
    public float Speed, MaxSpeed = 12, normalJumpForce;
    public float rotationSpeed = 120, rotateInput = 100;
    public float staminaAmount = 0;
    public float counterMovement;
    [Range(0, 2f)]public float staminaCooldown = 0;
    [HideInInspector]public Vector3 CamF;
    [HideInInspector]public Vector3 CamR;
    [HideInInspector]public Vector3 Movement;
    [HideInInspector]public float MovementX;
    [HideInInspector]public float MovementY;

    //public CinemachineRecomposer cinemachineRecomposer;
    private float PanRef;

    public Vector3 velocityXZ;

    [Header("References")]
    public Rigidbody rb;
    public Transform Camera;

    [Header("States")]
    public bool crouching;
    public bool isRunning;
    public bool cantRun;
    public bool going;

    public float targetTilt;
    public float blendTilt;

    private Quaternion targetRotation;
    public EnterDoor ed;

    void Awake()
    {
        Camera = GameObject.Find("PlayerCamera").transform;
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        ed = FindAnyObjectByType<EnterDoor>();
        //cinemachineRecomposer = FindAnyObjectByType<CinemachineRecomposer>();
        staminaAmount = 2;
        targetRotation = transform.rotation;
    }

    void Update()
    {
        //cinemachineRecomposer.Tilt = Mathf.SmoothDamp(cinemachineRecomposer.Tilt, -MovementY*2, ref PanRef, 0.5f);
        HandleMovement();
    }

    void FixedUpdate()
    {
        velocityXZ = new Vector3 (rb.linearVelocity.x, 0, rb.linearVelocity.z);

        Movement = (CamF * MovementY + CamR * MovementX).normalized;
        rb.AddForce(transform.forward * Speed * MovementY);
        rb.AddForce(velocityXZ * counterMovement);

        //staminaAmount = Math.Clamp (staminaAmount + (isRunning? -Time.deltaTime: +Time.deltaTime), 0, 2f);

        LockToMaxSpeed();

    }


    public void onMove(InputAction.CallbackContext MovementValue)
    {
        Vector2 inputVector = MovementValue.ReadValue<Vector2>();
        MovementY = inputVector.y;
        rotateInput = inputVector.x;

        going = MovementY != 0;
    }

    private void HandleMovement()
    {
        // rotate left & right
        if (going == false)
        {
            float rotationAmount = rotateInput * rotationSpeed * Time.deltaTime;
            targetRotation *= Quaternion.Euler (0f, rotationAmount, 0f);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1.2f);
    }

    public void Run(InputAction.CallbackContext run)
    {
        if(run.started)
        {
            isRunning = true;
            Speed = 100;
        }
        
        if(run.canceled)
        {
            isRunning = false;
            Speed = 70;
        }
    }

    public void Interact(InputAction.CallbackContext interact)
    {
        if (interact.started)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f, Physics.AllLayers, QueryTriggerInteraction.Collide);
            foreach (Collider item in colliders)
            {
                EnterDoor door = item.GetComponent<EnterDoor>();
                if (door != null) door.DoorEntered();
            }
        }
    }


    public void LockToMaxSpeed()
    {
        Vector3 newVelocity = rb.linearVelocity;
        newVelocity.y = 0f;
        newVelocity = Vector3.ClampMagnitude(newVelocity, MaxSpeed);
        newVelocity.y = rb.linearVelocity.y;
        rb.linearVelocity = newVelocity;
    }

    public void CancelRun()
    {
        cantRun = true;
        Speed = 40;
    }
}