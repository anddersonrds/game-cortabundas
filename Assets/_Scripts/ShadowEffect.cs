using UnityEngine;
using System.Collections;

public class ShadowEffect : MonoBehaviour
{
    private Material material;
    float timer;
    bool triggered;

    public float showPeriod = 1.0f;
    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find("ShadowEffect"));
        triggered = false;
        timer = 0.0f;
    }

    private void Update()
    {
        if (triggered)
        {
            timer += Time.deltaTime;
            if (timer > showPeriod)
                triggered = false;
        }
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (triggered)
            Graphics.Blit(source, destination, material);
        else
            Graphics.Blit(source, destination);
    }

    public void StartShader()
    {
        timer = 0.0f;
        triggered = true;
    }
}