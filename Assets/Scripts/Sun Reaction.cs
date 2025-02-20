using UnityEngine;

public class SunReaction : MonoBehaviour
{
    public AudioSource audioSource;
    public float scaleMax = 1.5f;
    public float scaleMin = 0.8f;
    public float audioMult = 1f;
    public float smoothSpeed = 5f;
    private Vector3 initialScale;
    private Renderer m_Renderer;
    public float minBrightness = 1f;
    public float maxBrightness = 3f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialScale = transform.localScale;
        m_Renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float[] spectrum = new float[256];
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        // Calculate intensity based on audio
        float intensity = Mathf.Clamp(spectrum[5] * audioMult, 0f, 1f);

        float target = Mathf.Lerp(scaleMin * initialScale[0], scaleMax * initialScale[0], intensity);

        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(target, target, target), Time.deltaTime * smoothSpeed);

        //m_Renderer.material.color[2] = Mathf.Lerp(minBrightness, maxBrightness, intensity);
    }
}
