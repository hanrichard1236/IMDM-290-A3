using UnityEngine;

public class AudioReact : MonoBehaviour
{
    public AudioSource audioSource;
    public float vertMax = 1.5f;
    public float vertMin = 0.8f;
    public float horMax = 1.2f;
    public float horMin = 0.8f;
    public float audioMult = 1f;
    public float smoothSpeed = 5f;
    private Vector3 initialScale;
    private Vector3 initialPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialScale = transform.localScale;
        initialPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.localPosition;
        // Get audio spectrum data
        float[] spectrum = new float[256];
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        float intensity = 0f;
        for (int i = 8; i < 256; i++)
        {
            intensity += spectrum[i];
        }
        intensity = Mathf.Clamp(intensity * audioMult, 0f, 1f);

        // Interpolate vertical and horizontal scaling
        float targetY = Mathf.Lerp(vertMin * initialScale[1], vertMax * initialScale[1], intensity);
        float targetX = Mathf.Lerp(horMin * initialScale[0], horMax * initialScale[0], 1f - intensity);
        float targetZ = targetX;

        // Smoothly scale the object
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(targetX, targetY, targetZ), Time.deltaTime * smoothSpeed);

        // Anchor the base by adjusting position
        float heightOffset = (transform.localScale.y - initialScale.y) / 2f;
        transform.localPosition = new Vector3(position[0], initialPos[1], position[2]) + new Vector3(0, heightOffset, 0);
    }

    void OnDrawGizmos()
    {
        if (audioSource == null) return;

        float[] spectrum = new float[256];
        audioSource.GetSpectrumData(spectrum, 0, FFTWindow.BlackmanHarris);

        float heightMultiplier = 50f; // Increase this value to make lines taller

        for (int i = 8; i < spectrum.Length; i++)
        {
            float x1 = (i - 1) * 0.1f;
            float y1 = spectrum[i - 1] * heightMultiplier;
            float x2 = i * 0.1f;
            float y2 = spectrum[i] * heightMultiplier;

            Gizmos.color = Color.Lerp(Color.blue, Color.red, (float)i / spectrum.Length);
            Gizmos.DrawLine(new Vector3(x1, y1, 0), new Vector3(x2, y2, 0));
        }
    }
}

