using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject spawner;
    public GameObject DeletePlane;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(DeletePlane.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == DeletePlane)
        {
            transform.position = spawner.transform.position;
        }
    }
}
