using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightPulser : MonoBehaviour {

    public float amplitude = 1.0f;
    public float frequency = 1.0f;
    private float intensity;
    private float tempIntensity;

    void Start () {
        intensity = GetComponent<Light>().intensity;
        tempIntensity = 0f;
	}
	
	void Update () {
        tempIntensity = intensity;
        tempIntensity += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        GetComponent<Light>().intensity = tempIntensity;
    }
}
