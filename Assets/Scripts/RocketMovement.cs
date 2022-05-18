using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
    [SerializeField] Vector3 movement;
    [SerializeField] float movementDuration = 3f;
    [SerializeField] float rotationDuration = 1f;
    Vector3 originalPosition;
    Vector3 destination;
    float rotationDegrees;
    bool returnMovement = false;
    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        LeanTween.cancel(gameObject);
        MoveTween();
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
            .setDelay(1f)
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
            .setDelay(1f)
            .setOnComplete(MoveTween);
    }
}
