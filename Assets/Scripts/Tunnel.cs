using UnityEngine;
using System.Collections;

public class Tunnel : MonoBehaviour
{
    private GameObject tunnel;
    private Quaternion initRot;
    public float delay;
    public float speed;
    public float deletetime;
    public float[] times;                // Times to start spawning
    public float[] transitionDurations; // How long to spawn per time

    void Start()
    {
        tunnel = gameObject;
        initRot = gameObject.transform.rotation;

        // Start coroutines based on times and durations
        for (int i = 0; i < times.Length && i < transitionDurations.Length; i++)
        {
            StartCoroutine(ScheduleSpawn(times[i], transitionDurations[i]));
        }
    }

    private IEnumerator ScheduleSpawn(float startTime, float duration)
    {
        // Wait until the start time
        yield return new WaitForSeconds(startTime);

        // Spawn objects for the duration
        float endTime = Time.time + duration;

        while (Time.time < endTime)
        {
            // Spawn object
            GameObject spawnedObject = Instantiate(tunnel, tunnel.transform.position, initRot);

            // Ensure Rigidbody for movement
            Rigidbody rb = spawnedObject.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = spawnedObject.AddComponent<Rigidbody>();
                rb.useGravity = false;
            }

            // Move right
            rb.linearVelocity = Vector3.right * speed;

            // Destroy after set time
            Destroy(spawnedObject, deletetime);

            // Wait for next spawn
            yield return new WaitForSeconds(delay);
        }
    }
}
