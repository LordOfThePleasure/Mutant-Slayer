using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected float speed;
    protected int damage;
    protected string opponentTag;

    public void Set(float speed, int damage, string opponentTag)
    {
        this.speed = speed;
        this.damage = damage;
        this.opponentTag = opponentTag;
    }

    virtual protected void Update()
    {
        MoveForward();
    }

    protected void MoveForward()
    {
        transform.position += speed * Time.deltaTime * transform.right;
    }

    protected virtual void OnHitTarget(Collider2D collision)
    {
        collision.GetComponent<Health>().TakeDamage(damage);
        gameObject.SetActive(false);
    }

    protected virtual void OnHitObsticle(Collider2D collision)
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(opponentTag))
        {
            OnHitTarget(collision);
        }
        else if (collision.CompareTag("Obsticle"))
        {
            OnHitObsticle(collision);
        }
    }
}
