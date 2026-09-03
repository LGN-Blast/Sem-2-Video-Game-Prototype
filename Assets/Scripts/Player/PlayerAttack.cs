using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    bool attack = false;
    public float timeBetweenAttack = 0.5f;
    float timeSinceAttack;

    

    void Update()
    {
        GetInputs();
        Attack();
    }

    void GetInputs()
    {
        attack = Input.GetKeyDown(KeyCode.Space);
    }

    void Attack()
    {
        timeSinceAttack += Time.deltaTime;

        if (attack && timeSinceAttack >= timeBetweenAttack)
        {
            timeSinceAttack = 0;
            animator.SetTrigger("Attack");
        }
        
    }
}
