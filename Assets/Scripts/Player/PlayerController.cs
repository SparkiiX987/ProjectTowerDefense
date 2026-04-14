using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    [SerializeField] private float moveSpeed;

    [SerializeField] private float minZoom;
    [SerializeField] private float maxZoom;
    [SerializeField] private float zoomSpeed;

    [SerializeField] private LayerMask interactibleMask;

    private InputSystem_Actions action;

    private Vector3 nextPosition;
    private Vector2 moveDirection;

    private Vector3 nextZoom;
    private float zoomDelta;

    private void Awake()
    {
        action = new();
        //move
        action.Player.Move.performed += Move;
        action.Player.Move.canceled += StopMove;
        //zoom
        action.Player.Zoom.performed += Zoom;
        action.Player.Zoom.canceled += StopZoom;
        //click
        action.Player.Attack.performed += Click;
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    private void Start()
    {

    }

    public void Click(InputAction.CallbackContext _ctx)
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.value);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, interactibleMask))
        {
            print($"{hit.collider.gameObject.name}");
        }
    }

    private void Move(InputAction.CallbackContext _ctx)
    {
        moveDirection = _ctx.ReadValue<Vector2>();
    }

    private void StopMove(InputAction.CallbackContext _ctx)
    {
        moveDirection = Vector2.zero;
    }

    private void Zoom(InputAction.CallbackContext _ctx)
    {
        zoomDelta = _ctx.ReadValue<Vector2>().y;
    }

    private void StopZoom(InputAction.CallbackContext _ctx)
    {
        zoomDelta = 0;
    }

    private void Update()
    {
        ApplyMovement();

        ApplyZoom();
        
    }

    private void ApplyMovement()
    {
        nextPosition.Set(transform.position.x + moveDirection.x * moveSpeed,
            transform.position.y,
            transform.position.z + moveDirection.y * moveSpeed);
        transform.position = nextPosition;
    }

    private void ApplyZoom()
    {
        nextZoom.Set(cameraTransform.localPosition.x,
            cameraTransform.localPosition.y - zoomDelta * zoomSpeed,
            cameraTransform.localPosition.z);

        if (nextZoom.y > maxZoom) nextZoom.y = maxZoom;
        if (nextZoom.y < minZoom) nextZoom.y = minZoom;

        cameraTransform.localPosition = nextZoom;
    }
}
