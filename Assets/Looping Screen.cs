using UnityEngine;

public class LoopingScreen : MonoBehaviour
{
    public GameObject[] objects;
    public float speed;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < objects.Length; i ++)
        {
            objects[i].GetComponent<Rigidbody>().linearVelocity = new Vector3(speed, 0, 0);
            objects[i].transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
