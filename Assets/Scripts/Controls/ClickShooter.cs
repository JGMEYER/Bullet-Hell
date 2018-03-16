using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ClickShooter : MonoBehaviour {

    [SerializeField] private GameObject bulletPrefab;

    public bool displayLine = true;
    public float lineLength = 3f;
    public float snapAngle = 45f;
    public float bulletDelay = 0.5f;
    private float _timeSinceLastBullet;

    void Start () {
        _timeSinceLastBullet = bulletDelay;	
	}

    void Update() {
        _timeSinceLastBullet += Time.deltaTime;

        if (_timeSinceLastBullet > bulletDelay) {
            _timeSinceLastBullet = bulletDelay;
        }

        // plane along the y-axis
        Plane hPlane = new Plane(Vector3.up, Vector3.zero);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        float distance;
        if (hPlane.Raycast(ray, out distance)) {
            // find where click intersects y-axis
            Vector3 point = ray.GetPoint(distance);

            Vector3 heading = point - transform.position;
            heading.y = 0;
            heading = SnapTo(heading, snapAngle);

            if (displayLine) {
                RenderFireDirectionLine(heading);
            }

            if (Input.GetMouseButton(1) && _timeSinceLastBullet >= bulletDelay) {
                FireBullet(heading);
            }
        }
    }

    private void RenderFireDirectionLine(Vector3 heading) {
        Vector3 start = transform.position + heading.normalized;
        Vector3 end = start + heading.normalized * lineLength;

        LineRenderer line = transform.GetComponent<LineRenderer>();
        line.SetPosition(0, start);
        line.SetPosition(1, end);
    }

    private void FireBullet(Vector3 heading) {
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.LookRotation(heading);

        _timeSinceLastBullet = 0;
    }

    // Snaps a vector to the nearest specified angle along the horizonal plane.
    // Copied from the internet and modified.
    Vector3 SnapTo(Vector3 heading, float snapAngle) {
        float angle = Vector3.Angle(heading, Vector3.forward);

        // cannot do cross product with angles 0 & 180
        if (angle < snapAngle / 2.0f) {
            return Vector3.forward * heading.magnitude;
        }
        if (angle > 180.0f - snapAngle / 2.0f) {
            return Vector3.back * heading.magnitude;
        }

        float t = Mathf.Round(angle / snapAngle);
        float deltaAngle = (t * snapAngle) - angle;

        Vector3 axis = Vector3.Cross(Vector3.forward, heading);
        Quaternion q = Quaternion.AngleAxis(deltaAngle, axis);
        return q * heading;
    }
}
