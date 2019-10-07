using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage=30;
    [SerializeField] private int hp=100;

    public void dealDamage(int damage2deal)
    {
        if (damage2deal > 0)
        {
            hp -= damage2deal;
        }

        if (hp <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Cell"))
        {
            collision.gameObject.GetComponent<Cell>().dealDamage(damage);
            gameObject.GetComponent<EnemyControl>().bounce();
        }
    }
}





