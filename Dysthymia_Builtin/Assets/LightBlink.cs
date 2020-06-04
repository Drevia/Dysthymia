using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : MonoBehaviour
{

    public Light light;

    public float firstBlink;
    public float secondBlink;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator Blink()
    {
        while (true)
        {
            light.intensity = firstBlink;
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
            light.intensity = secondBlink;
            yield return new WaitForSeconds(Random.Range(1f, 5f));

        }
    }

   public void goBlink()
    {
        StartCoroutine("Blink");
    }
}
