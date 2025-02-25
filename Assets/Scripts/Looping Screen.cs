using UnityEngine;

public class LoopingScreen : MonoBehaviour
{
    public GameObject[] objects;
    private Quaternion[] initialRotations;
    public float speed;
    public bool isPaused = false;
    
    void Start()
    {
        initialRotations = new Quaternion[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
            {
                initialRotations[i] = objects[i].transform.rotation;
            }
        }
    }
    
    void Update()
    {
        // Skip if paused
        if (isPaused)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] != null)
                {
                    Rigidbody rb = objects[i].GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.linearVelocity = new Vector3(0, 0, 0);
                        rb.MoveRotation(initialRotations[i]);
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] != null)
                {
                    Rigidbody rb = objects[i].GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.linearVelocity = new Vector3(speed, 0, 0);
                        rb.MoveRotation(initialRotations[i]);
                    }
                }
            }
        }
    }
}