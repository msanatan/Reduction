using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float snapDistance = 1f;
    [SerializeField] float scaleReduction = 0.1f;
    [SerializeField] Transform cameraTransform;
    public UnityEvent gameOverEvent;
    Vector3 targetPosition;
    Vector3 startPosition;
    Vector3 scaleVector;
    bool moving = false;

    // Start is called before the first frame update
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

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetPosition = transform.position + Vector3.left;
            if (Physics.CheckSphere(targetPosition, 0.5f))
            {
                startPosition = transform.position;
                moving = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetPosition = transform.position + Vector3.right;
            if (Physics.CheckSphere(targetPosition, 0.5f))
            {
                startPosition = transform.position;
                moving = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            targetPosition = transform.position + Vector3.forward;
            if (Physics.CheckSphere(targetPosition, 0.5f))
            {
                startPosition = transform.position;
                moving = true;
            }
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            targetPosition = transform.position + Vector3.back;
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
        // Move the cube's position down so it continues to touch the floor
        transform.position -= new Vector3(0, scaleReduction / 2, 0);
        CheckGameOver();
    }

    void CheckGameOver()
    {
        if (transform.localScale.x <= 0)
        {
            Debug.Log("Game over");
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
