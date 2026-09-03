using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;

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
        health -= _damageDealt;
    }
}
