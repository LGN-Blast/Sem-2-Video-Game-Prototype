using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (health <= 0)
        {
            EnemySpawner.Instance.enemiesAlive--;
            Destroy(gameObject);
        }
    }

    public void EnemyHit(float _damageDealt, Vector2 _hitDirection, float _hitForce)
    {
        // Damage
        health -= _damageDealt;

        // Knockback
        rb.AddForce(-_hitDirection * 0.01f * _hitForce, ForceMode2D.Impulse);
    }
}
