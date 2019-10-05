using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage;

    private void Start() {
        damage = 30;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Cell"))
        {
            collision.gameObject.GetComponent<Cell>().dealDamage(damage);
            // GameObject.Destroy(collision.gameObject);
            // Debug.Log($"{collision.gameObject.name} destroyed");
        }
    }
}





