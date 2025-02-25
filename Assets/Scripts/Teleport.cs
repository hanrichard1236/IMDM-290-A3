using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject spawner;

    public GameObject DeletePlane;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == DeletePlane)
        {
            Vector3 initialPosition = transform.position;
            transform.position = new Vector3(spawner.transform.position.x, initialPosition.y, initialPosition.z);
        }
    }
}
