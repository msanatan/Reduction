using UnityEngine;
using UnityEngine.Events;

public class FinalBlock : MonoBehaviour
{
    public UnityEvent levelCompleteEvent;

    private void Start()
    {
        if (levelCompleteEvent == null)
        {
            levelCompleteEvent = new UnityEvent();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Level complete");
            Destroy(gameObject);
            levelCompleteEvent.Invoke();
        }
    }
}
