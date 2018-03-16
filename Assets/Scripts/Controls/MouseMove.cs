using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour {
    public float speed = 6f;
    private CharacterController _charController;

    void Start() {
        _charController = GetComponent<CharacterController>();
    }

    void Update() {
        if (Input.GetMouseButton(0)) {
            // plane along the y-axis
            Plane hPlane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float distance;
            if (hPlane.Raycast(ray, out distance)) {
                // find where click intersects y-axis
                Vector3 point = ray.GetPoint(distance);

                Vector3 heading = point - transform.position;
                heading.y = 0;
                heading = Vector3.ClampMagnitude(heading, speed);
                heading = transform.TransformDirection(heading * Time.deltaTime);
                _charController.Move(heading);
            }
        }
    }
}
