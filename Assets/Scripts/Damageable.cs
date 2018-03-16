using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

    [SerializeField] private int _health = 1;

    public bool destroyOnKill = false;

    public int GetHealth() {
        return _health;
    }

    public void Hurt(int damage) {
        _health -= damage;

        if (_health <= 0) {
            _health = 0;

            if (destroyOnKill) {
                Destroy(this.gameObject);
            }
        }
    }
}
