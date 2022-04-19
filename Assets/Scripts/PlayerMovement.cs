using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float snapDistance = 1f;
    [SerializeField] float scaleReduction = 0.1f;
    [SerializeField] Transform cameraTransform;
    UnityEvent gameOverEvent;
    Vector3 targetPosition;
    Vector3 startPosition;
    Vector3 cameraRight;
    Vector3 cameraForward;
    Vector3 scaleVector;
    bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        scaleVector = new Vector3(scaleReduction, scaleReduction, scaleReduction);
        cameraRight = cameraTransform.right;
        cameraRight.y = 0;
        cameraRight.z = 0;
        cameraRight.Normalize();
        cameraForward = cameraTransform.forward;
        cameraForward.x = 0;
        cameraForward.y = 0;
        cameraForward.Normalize();

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

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetPosition = transform.position - cameraRight;
            if (Physics.CheckSphere(targetPosition, 0.5f))
            {
                startPosition = transform.position;
                moving = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetPosition = transform.position + cameraRight;
            if (Physics.CheckSphere(targetPosition, 0.5f))
            {
                startPosition = transform.position;
                moving = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            targetPosition = transform.position + cameraForward;
            if (Physics.CheckSphere(targetPosition, 0.5f))
            {
                startPosition = transform.position;
                moving = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            targetPosition = transform.position - cameraForward;
            if (Physics.CheckSphere(targetPosition, 0.5f))
            {
                startPosition = transform.position;
                moving = true;
            }
        }
    }

    void ReducePlayer()
    {
        transform.localScale -= scaleVector;
        if (transform.localScale.x <= 0)
        {
            Debug.Log("Game over");
            gameOverEvent.Invoke();
        }
    }
}
