using UnityEngine;
using System.Collections;

public class RadialBlur : MonoBehaviour
{
    private Material material;
    float timer;
    bool hit;

    public float hitPeriod = 5.0f;
    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Shader.Find("RadialBlur"));
        hit = false;
        timer = 0.0f;
    }

    private void Update()
    {
        if (hit)
        {
            timer += Time.deltaTime;
            if (timer > hitPeriod)
                hit = false;
        }
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (hit)
            Graphics.Blit(source, destination, material);
        else
            Graphics.Blit(source, destination);
    }

    public void StartShader()
    {
        timer = 0.0f;
        hit = true;
    }
}