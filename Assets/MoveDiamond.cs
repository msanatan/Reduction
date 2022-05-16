using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDiamond : MonoBehaviour
{
    [SerializeField] float speed = 1.0f;
    [SerializeField] float originalY = 0.5f;
    [SerializeField] float maxHeight = 0.25f;

    // Update is called once per frame
    void Update()
    {
        float y = Mathf.PingPong(Time.time * speed, maxHeight) + originalY;
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }
}
