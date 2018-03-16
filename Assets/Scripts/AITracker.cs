using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITracker : MonoBehaviour {

    [SerializeField] private GameObject _target;

    public float speed = 3.0f;

    void Update () {
        if (_target) {
            Vector3 heading = _target.transform.position - transform.position;
            heading.y = 0;
            heading = Vector3.ClampMagnitude(heading, speed);
            transform.position += heading * Time.deltaTime;
        }
	}

    public void SetTarget(GameObject target) {
        _target = target;
    }

    public GameObject GetTarget() {
        return _target;
    }
}
