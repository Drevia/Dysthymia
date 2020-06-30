using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    public float intensityVariation = 0.3f;
    public float rangeVariation = 0.3f;
    float originIntensity;
    float originRange;
    Light light;
    [Range(0f, 1f)]
    public float turnOffProbability = 0;
    public float variationSpeed = 1f;
    float wantedIntensity;
    float wantedRange;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        originIntensity = light.intensity;
        originRange = light.range;
        if (intensityVariation > 0)
        {
            VariateLight();
        }
    }

    // Update is called once per frame
    void Update()
    {
        light.enabled = Random.Range(0f, 1f) > turnOffProbability;

        if (intensityVariation > 0)
        {
            //light.intensity = Mathf.Lerp(light.intensity, wantedIntensity, Time.deltaTime * variationSpeed);
            light.intensity = Mathf.MoveTowards(light.intensity, wantedIntensity, Time.deltaTime * variationSpeed);
            light.range = Mathf.MoveTowards(light.range, wantedRange, Time.deltaTime * variationSpeed);
            
            if (Mathf.Abs(light.intensity - wantedIntensity) < 0.05f)
            {
                VariateLight();
            }
        }
    }

    void VariateLight()
    {
        float rand = Random.Range(-1f, 1f);
        wantedIntensity = originIntensity + rand * intensityVariation;
        wantedRange = originRange + rand * rangeVariation;
    }
}
