using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject spawner;
    private Vector3 initialPosition;
    public GameObject DeletePlane;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == DeletePlane)
        {
            transform.position = new Vector3(spawner.transform.position.x, initialPosition.y, initialPosition.z);
        }
    }
}
