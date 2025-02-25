using UnityEngine;
public class SceneChange : MonoBehaviour
{
    public GameObject[] scenes;
    public Material[] skyboxes;
    public float[] cutoffs;
    
    // Array of custom offscreen positions for each scene
    public Vector3[] onscreenPositions;
    public Vector3[] offscreenPositions;
    
    // Default fallback position if array is not fully configured
    public Vector3 defaultOffscreenPosition = new Vector3(10000, 10000, 10000);
    
    private float time;
    private int currentSceneIndex = -1;
   
    void Start()
    {
        // Move all scenes offscreen initially
        for (int i = 0; i < scenes.Length; i++)
        {
            GameObject scene = scenes[i];
            if (scene != null)
            {
                // Move to designated offscreen position
                scene.transform.position = GetOffscreenPosition(i);
            }
        }
    }
   
    void Update()
    {
        time += Time.deltaTime;
        for (int i = 0; i < cutoffs.Length; i++)
        {
            if (time >= cutoffs[i] && currentSceneIndex != i)
            {
                SwitchScenePosition(i);
            }
        }
    }
   
    void SwitchScenePosition(int index)
    {
        // Move current scene offscreen if there is one
        if (currentSceneIndex != -1 && currentSceneIndex < scenes.Length)
        {
            GameObject currentScene = scenes[currentSceneIndex];
           
            // Pause any looping screens
            PauseLoopingScreens(currentScene, true);
           
            // Move to designated offscreen position
            currentScene.transform.position = GetOffscreenPosition(currentSceneIndex);
        }
       
        // Move new scene onscreen
        if (index < scenes.Length && scenes[index] != null)
        {
            GameObject newScene = scenes[index];
           
            // Move to original position
            newScene.transform.position = onscreenPositions[index];
            
            // Unpause any looping screens
            PauseLoopingScreens(newScene, false);
            
            currentSceneIndex = index;
        }
       
        // Change skybox if available
        if (index < skyboxes.Length && skyboxes[index] != null)
        {
            RenderSettings.skybox = skyboxes[index];
            DynamicGI.UpdateEnvironment(); // Update lighting for the new skybox
        }
    }
   
    Vector3 GetOffscreenPosition(int sceneIndex)
    {
        if (sceneIndex >= 0 && sceneIndex < offscreenPositions.Length)
        {
            return offscreenPositions[sceneIndex];
        }
        
        return defaultOffscreenPosition; // Default fallback
    }
   
    void PauseLoopingScreens(GameObject sceneRoot, bool pause)
    {
        LoopingScreen[] loopingScreens = sceneRoot.GetComponentsInChildren<LoopingScreen>();
        foreach (LoopingScreen screen in loopingScreens)
        {
            screen.isPaused = pause;
        }
    }
}
