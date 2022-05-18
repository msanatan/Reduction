using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] Vector3 movement;
    [SerializeField] float movementDuration = 3f;
    [SerializeField] float rotationDuration = 1f;
    [SerializeField] float movementDelay = 0.5f;
    [SerializeField] float rotationDelay = 0.5f;
    [SerializeField] float startDelay = 0;
    Vector3 originalPosition;
    Vector3 destination;
    float rotationDegrees;
    bool returnMovement = false;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        LeanTween.cancel(gameObject);

        if (startDelay != 0)
        {
            Invoke("MoveTween", startDelay);
        }
        else
        {
            MoveTween();
        }
    }

    void MoveTween()
    {
        if (returnMovement)
        {
            destination = originalPosition;
        }
        else
        {
            destination = movement;
        }

        LeanTween.move(gameObject, destination, movementDuration)
            .setDelay(movementDelay)
            .setOnComplete(RotateAndPause);
    }

    void RotateAndPause()
    {
        if (transform.rotation.y < 0)
        {
            rotationDegrees = -180f;
        }
        else
        {
            rotationDegrees = 180f;
        }

        returnMovement = !returnMovement; // Flip flag after rotation

        LeanTween.rotateAroundLocal(gameObject, Vector3.left, rotationDegrees, rotationDuration)
            .setDelay(rotationDelay)
            .setOnComplete(MoveTween);
    }
}
