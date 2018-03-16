using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField] private GameObject targetObject;

    public float offsetX;
    public float offsetY;
    public float offsetZ;
    public float speed = 6f;
    private Camera _camera;

    void Start() {
        _camera = GetComponent<Camera>();

        Vector3 targetPosition = targetObject.transform.position;
        Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);
        _camera.transform.position = targetPosition + offset;
        _camera.transform.LookAt(targetPosition);
    }

    void LateUpdate() {
        // plane along the y-axis
        Plane hPlane = new Plane(Vector3.up, Vector3.zero);

        Vector3 curPoint = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
        Ray ray = _camera.ScreenPointToRay(curPoint);

        float distance;
        if (hPlane.Raycast(ray, out distance)) {
            // find where camera intersects y-axis
            curPoint = ray.GetPoint(distance);

            Vector3 newPoint = targetObject.transform.position;
            Vector3 heading = newPoint - curPoint;
            heading.y = 0;
            Vector3.ClampMagnitude(heading, speed);
            transform.position += heading * Time.deltaTime;
        }
    }
}
