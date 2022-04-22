using UnityEngine;
using UnityEngine.Events;

public class FinalBlock : MonoBehaviour
{
    public UnityEvent levelCompleteEvent;

    private void Start()
    {
        levelCompleteEvent = new UnityEvent();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Level complete");
            levelCompleteEvent.Invoke();
        }
    }
}
