using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Cell"))
        {
            GameObject.Destroy(collider.gameObject);
            Debug.Log($"{collider.gameObject.name} destroyed");
        }
    }
}





