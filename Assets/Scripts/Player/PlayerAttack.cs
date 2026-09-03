using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;
    bool attack = false;
    float timeBetweenAttack, timeSinceAttack;
    [SerializeField] Transform SideAttackTransform;
    [SerializeField] Vector2 SideAttackArea;
    [SerializeField] LayerMask attackableLayer;
    [SerializeField] float damage =5f;


    PlayerMovement pm;

    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

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

            UpdateAttackDirection();

            Hit(SideAttackTransform, SideAttackArea);
        }
    }

    void UpdateAttackDirection()
    {
        Vector2 direction = pm.moveDir;

        if (direction == Vector2.zero)
        {
            direction = new Vector2(pm.lastHorizontalVector, pm.lastVerticalVector).normalized;
        }

        SideAttackTransform.localPosition = direction * 0.5f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(SideAttackTransform.position, SideAttackArea);
    }
    private void Hit(Transform _attackTransform, Vector2 _attackArea)
    {
        Collider2D[] ObjectsToHit = Physics2D.OverlapBoxAll(_attackTransform.position, _attackArea, 0,attackableLayer);

        Debug.Log("Objects detected: " + ObjectsToHit.Length);

        foreach (Collider2D obj in ObjectsToHit)
        {
            Debug.Log("Hit: " + obj.name);
        }

        for (int i = 0; i < ObjectsToHit.Length; i++)
        {
            if(ObjectsToHit[i].GetComponent<EnemyHealth>() !=null)
            {
                ObjectsToHit[i].GetComponent<EnemyHealth>().EnemyHit
                    (damage, (transform.position - ObjectsToHit[i].transform.position).normalized, 100);
            }    
        }

    }
}
