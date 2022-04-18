using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float snapDistance = 1f;
    [SerializeField] Transform cameraTransform;
    Vector3 targetPosition;
    Vector3 startPosition;
    Vector3 cameraRight;
    Vector3 cameraForward;
    bool moving = false;

    // Start is called before the first frame update
    void Start()
    {
        cameraRight = cameraTransform.right;
        cameraRight.y = 0;
        cameraRight.z = 0;
        cameraRight.Normalize();
        cameraForward = cameraTransform.forward;
        cameraForward.x = 0;
        cameraForward.y = 0;
        cameraForward.Normalize();
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
                return;
            }

            transform.position += (targetPosition - startPosition) * moveSpeed * Time.deltaTime;
            return;
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetPosition = transform.position - cameraRight;
            startPosition = transform.position;
            moving = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetPosition = transform.position + cameraRight;
            startPosition = transform.position;
            moving = true;
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            targetPosition = transform.position + cameraForward;
            startPosition = transform.position;
            moving = true;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            targetPosition = transform.position - cameraForward;
            startPosition = transform.position;
            moving = true;
        }
    }
}
