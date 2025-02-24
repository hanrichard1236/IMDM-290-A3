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
    private Color initialEmissionColor;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialScale = transform.localScale;
        m_Renderer = GetComponent<Renderer>();
        initialEmissionColor = m_Renderer.material.GetColor("_EmissionColor");
    }

    // Update is called once per frame
    void Update()
    {
        float[] spectrum = new float[256];
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        float bassIntensity = Mathf.Clamp(spectrum[2] * audioMult, 0f, 1f);
        float target = Mathf.Lerp(scaleMin * initialScale[0], scaleMax * initialScale[0], bassIntensity);
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(target, target, target), Time.deltaTime * smoothSpeed);

        float emissionStrength = Mathf.Lerp(minBrightness, maxBrightness, bassIntensity);
        m_Renderer.material.SetColor("_EmissionColor", initialEmissionColor * emissionStrength);
        DynamicGI.SetEmissive(m_Renderer, initialEmissionColor * emissionStrength);
    }
}
