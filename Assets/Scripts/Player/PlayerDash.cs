using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    //Dash Variables
    public Animator animator;
    public float dashSpeed = 20f;
    public float dashTime = 0.15f;
    public float dashCooldown = 1f;

    //references
    Rigidbody2D rb;
    PlayerMovement pm;
    bool candash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && candash)
        {
            StartCoroutine(Dash());
        }
    }

    IEnumerator Dash()
    {
        candash = false;
        pm.isDashing = true;
        animator.SetTrigger("Dashing");
        gameObject.layer = LayerMask.NameToLayer("PlayerInvincible");

        Vector2 dashDir = new Vector2(pm.lastHorizontalVector, pm.lastVerticalVector).normalized;
        rb.velocity = dashDir * dashSpeed;

        yield return new WaitForSeconds(dashTime);

        pm.isDashing = false;
        gameObject.layer = LayerMask.NameToLayer("Player");

        yield return new WaitForSeconds(dashCooldown);
        candash = true;
    }
}
