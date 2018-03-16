using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damager))]
public class Bullet : MonoBehaviour {

    public float speed = 10.0f;

    void Update() {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
