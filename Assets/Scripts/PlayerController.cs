using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float snapDistance = 1f;
    [SerializeField] float scaleReduction = 0.1f;
    [SerializeField] float minimumSwipeMagnitude = 10f;
    [SerializeField] Transform cameraTransform;
    public UnityEvent gameOverEvent;
    PlayerMovement input = null;
    Vector2 moveVector = Vector2.zero;
    Vector2 swipeDirection;
    Vector3 targetPosition;
    Vector3 startPosition;
    Vector3 scaleVector;
    bool moving = false;
    bool gameOver = false;

    void Awake()
    {
        input = new PlayerMovement();
    }

    void OnEnable()
    {
        input.Enable();
        input.Player.Keys.performed += OnMovementPerformed;
        input.Player.Keys.canceled += OnMovementCancelled;
        input.Player.Swipe.performed += OnSwipeDeltaPerformed;
        input.Player.Touch.canceled += OnTouchCancelled;
    }

    void OnDisable()
    {
        input.Enable();
        input.Player.Keys.performed -= OnMovementPerformed;
        input.Player.Keys.canceled -= OnMovementCancelled;
        input.Player.Swipe.performed -= OnSwipeDeltaPerformed;
        input.Player.Touch.canceled -= OnTouchCancelled;
    }

    private void OnMovementCancelled(InputAction.CallbackContext context)
    {
        return;
    }

    private void OnMovementPerformed(InputAction.CallbackContext context)
    {
        if (moving || gameOver) return; // No new inputs while moving

        moveVector = context.ReadValue<Vector2>();
        if (moveVector.y == 1)
        {
            targetPosition = transform.position + Vector3.forward;
        }
        else if (moveVector.y == -1)
        {
            targetPosition = transform.position + Vector3.back;
        }
        else if (moveVector.x == 1)
        {
            targetPosition = transform.position + Vector3.right;
        }
        else if (moveVector.x == -1)
        {
            targetPosition = transform.position + Vector3.left;
        }

        if (Physics.CheckSphere(targetPosition, 0.5f))
        {
            startPosition = transform.position;
            moving = true;
        }
    }

    private void OnSwipeDeltaPerformed(InputAction.CallbackContext context)
    {
        swipeDirection = context.ReadValue<Vector2>();
    }

    private void OnTouchCancelled(InputAction.CallbackContext context)
    {
        // Ignore if swipe isn't strong enough
        if (Mathf.Abs(swipeDirection.magnitude) < minimumSwipeMagnitude) return;

        if (moving || gameOver) return; // No new inputs while moving

        if (Mathf.Abs(swipeDirection.y) > Mathf.Abs(swipeDirection.x))
        {
            if (swipeDirection.y > 0)
            {
                targetPosition = transform.position + Vector3.forward;
            }
            else if (swipeDirection.y < 0)
            {
                targetPosition = transform.position + Vector3.back;
            }
        }
        else
        {
            if (swipeDirection.x > 0)
            {
                targetPosition = transform.position + Vector3.right;
            }
            else if (swipeDirection.x < 0)
            {
                targetPosition = transform.position + Vector3.left;
            }

        }

        if (Physics.CheckSphere(targetPosition, 0.5f))
        {
            startPosition = transform.position;
            moving = true;
        }
    }

    void Start()
    {
        scaleVector = new Vector3(scaleReduction, scaleReduction, scaleReduction);
        if (gameOverEvent == null)
        {
            gameOverEvent = new UnityEvent();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if (Vector3.Distance(startPosition, transform.position) > snapDistance)
            {
                transform.position = targetPosition;
                moving = false;
                ReducePlayer();
                return;
            }

            transform.position += (targetPosition - startPosition) * moveSpeed * Time.deltaTime;
            return;
        }
    }

    void ReducePlayer()
    {
        transform.localScale -= scaleVector;
        // Move the cube's position down so it continues to touch the floor
        transform.position -= new Vector3(0, scaleReduction / 2, 0);
        CheckGameOver();
    }

    void CheckGameOver()
    {
        if (transform.localScale.x <= 0)
        {
            Debug.Log("Game over");
            gameOver = true;
            gameOverEvent.Invoke();
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "Obstacle")
        {
            Destroy(collider.gameObject);
            transform.localScale -= scaleVector * 2;
            transform.position -= new Vector3(0, scaleReduction, 0);
            CheckGameOver();
        }
    }
}
