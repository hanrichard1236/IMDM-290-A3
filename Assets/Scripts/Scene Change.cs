using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public GameObject[] scenes;
    public float[] cutoffs;
    public GameObject transition;
    private float time;
    private int currentSceneIndex = -1;

    void Start()
    {
        foreach (GameObject scene in scenes)
        {
            if (scene != null)
                scene.SetActive(false);
        }
    }

    void Update()
    {
        time += Time.deltaTime;

        for (int i = 0; i < cutoffs.Length; i++)
        {
            if (time >= cutoffs[i] && currentSceneIndex != i)
            {
                ChangeScene(i);
            }
        }
    }

    void ChangeScene(int index)
    {
        if (currentSceneIndex != -1 && currentSceneIndex < scenes.Length)
        {
            scenes[currentSceneIndex].SetActive(false);
        }

        // Activate the new scene
        if (index < scenes.Length)
        {
            scenes[index].SetActive(true);
            currentSceneIndex = index;
        }
    }
}
