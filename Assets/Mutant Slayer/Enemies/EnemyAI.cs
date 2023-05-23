using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OpponentDetector))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] protected float visionDistance, attackDistance, attackRange;
    [SerializeField] protected float playerDetectorRate;
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;

    [SerializeField] protected OpponentDetector detector;
    [SerializeField] protected Transform gfx;
    [SerializeField] protected Animator anim;

    [SerializeField] protected LayerMask playerLayerMask;

    protected Transform currentTarget;

    protected void Start()
    {
        InvokeRepeating(nameof(FindPlayer), playerDetectorRate, playerDetectorRate);
    }

    private void Update()
    {
        if (currentTarget == null)
        {
            return;
        }

        gfx.transform.localScale = transform.position.x > currentTarget.position.x ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);

        if (Vector2.Distance(transform.position, currentTarget.position) < attackDistance)
        {
            Attack();
        }
        else
        {
            ChacePlayer();
        }
    }

    protected void FindPlayer()
    {
        currentTarget = detector.FindTarget(visionDistance);
    }

    protected void ChacePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, speed * Time.deltaTime);
    }

    protected void Attack()
    {
        anim.Play("Attack");
    }

    // used by animation event
    protected void DealDamage()
    {
        Collider2D collider = Physics2D.OverlapCircle(transform.position, attackRange, playerLayerMask);

        if (collider == null)
        {
            return;
        }

        collider.GetComponent<Health>().TakeDamage(damage);
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
