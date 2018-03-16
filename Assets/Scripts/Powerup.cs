using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Powerup : MonoBehaviour {
    // NOTE: Behavior could be further generalized into a BuffDebuff component
    // along with a specified script type that would be impacted. Powerup would
    // then just be a basic collision detector that applies the BuffDebuff
    // accordingly.

    public float snapAngle = 1f;
    public float bulletDelay = 0.01f;
    public float buffDuration = 5f;
    private float savedSnapAngle;
    private float savedBulletDelay;
    private bool activated;

    void Start() {
        activated = false;
    }

    void OnTriggerEnter(Collider other) {
        if (!other.GetComponent<PlayerCharacter>() || activated) {
            return;
        }

        ClickShooter clickShooter = other.GetComponent<ClickShooter>();
        if (clickShooter) {
            StartCoroutine(ApplyBuff(clickShooter));
        }
    }

    private IEnumerator ApplyBuff(ClickShooter clickShooter) {
        activated = true;

        foreach (Renderer childRenderer in GetComponentsInChildren<Renderer>()) {
            childRenderer.enabled = false;
        }
        foreach (Light childLight in GetComponentsInChildren<Light>()) {
            childLight.enabled = false;
        }

        savedSnapAngle = clickShooter.snapAngle;
        savedBulletDelay = clickShooter.bulletDelay;

        clickShooter.snapAngle = snapAngle;
        clickShooter.bulletDelay = bulletDelay;

        yield return new WaitForSeconds(buffDuration);

        clickShooter.snapAngle = savedSnapAngle;
        clickShooter.bulletDelay = savedBulletDelay;

        Destroy(gameObject);
    }
}
