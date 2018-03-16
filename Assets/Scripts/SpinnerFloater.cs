using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerFloater : MonoBehaviour {

    public float degreesPerSecond = 5.0f;
    public float amplitude = 1.0f;
    public float frequency = 1.0f;
    private Vector3 posOffset;
    private Vector3 tempPos;

    private void Start() {
        posOffset = transform.position;
        tempPos = new Vector3();
    }

    void Update () {
        Vector3 rotationVector = new Vector3(0f, degreesPerSecond * Time.deltaTime, 0f);
        transform.Rotate(rotationVector, Space.World);

        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
	}
}
