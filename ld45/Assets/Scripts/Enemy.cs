using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage=30;
    [SerializeField] private int hp=100;
    private GameObject lastCollider;
    private float timeOfLastAttack=0.0f;
    [SerializeField] private float attackRate;

    private void Start() 
    {
        lastCollider = gameObject;
    }
    //Returns True if the enemy has been destroyed
    public bool dealDamage(int damage2deal)
    {
        if (damage2deal > 0)
        {
            hp -= damage2deal;
        }

        if (hp <= 0)
        {
            GameObject.Destroy(gameObject);
            return true;
        }
        return false;
    }

    private bool stillTouching()
    {
        if (lastCollider!=gameObject)
        {
            if (gameObject.GetComponent<Collider2D>().IsTouching(lastCollider.GetComponent<Collider2D>()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private void checkAndDealDamageToCollider()
    {
        if (stillTouching())
        {
            if (Time.time > timeOfLastAttack + attackRate)
            {
                if (lastCollider.CompareTag("Cell"))
                {
                    bool killed = lastCollider.GetComponent<Cell>().dealDamage(damage);
                    if (killed)
                    {
                        lastCollider = gameObject;
                    }
                    // gameObject.GetComponent<EnemyControl>().bounce();
                    timeOfLastAttack = Time.time;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        checkAndDealDamageToCollider();
    }
    

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        lastCollider = collision.gameObject;
    }
}





