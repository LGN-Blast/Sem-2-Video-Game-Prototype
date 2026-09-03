using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public int damage = 1;
    public float damageCooldown = 1f; // seconds between hits

    private float lastHitTime = -999f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (Time.time - lastHitTime < damageCooldown) return;

        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.ChangeHealth(-damage);
            lastHitTime = Time.time;
        }
    }
}