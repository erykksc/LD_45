using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision) {
        //Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Cell"))
        {
            GameObject.Destroy(collision.gameObject);
            //Debug.Log($"{collision.gameObject.name} destroyed");
        }
    }
}





