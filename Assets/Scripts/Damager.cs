using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Damager : MonoBehaviour {

    public int damage = 1;
    public bool hurtsPlayer = false;
    public bool hurtsEnemy = false;
    public bool destroyOnDamage = false;

    void OnTriggerEnter(Collider other) {
        Damageable damageable = other.GetComponent<Damageable>();

        if (damageable) {
            PlayerCharacter player = other.GetComponent<PlayerCharacter>();
            EnemyCharacter enemy = other.GetComponent<EnemyCharacter>();

            if (player != null && hurtsPlayer) {
                damageable.Hurt(damage);
                if (gameObject && destroyOnDamage) {
                    Destroy(gameObject);
                }
            }

            if (enemy != null && hurtsEnemy) {
                damageable.Hurt(damage);
                if (gameObject && destroyOnDamage) {
                    Destroy(gameObject);
                }
            }
        } else {
            Destroy(gameObject);
        }
    }
}
