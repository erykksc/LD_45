using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int hp;

    private void Start() {
        damage = 30;
    }

    public void dealDamage(int damage2deal)
    {
        if (damage > 0)
        {
            hp -= damage;
        }

        if (hp <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Cell"))
        {
            collision.gameObject.GetComponent<Cell>().dealDamage(damage);
            // c
            // Debug.Log($"{collision.gameObject.name} destroyed");
        }
    }
}





