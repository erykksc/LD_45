using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Cell"))
        {
            GameObject.Destroy(GetComponent<Collider>().gameObject);
            Debug.Log($"{GetComponent<Collider>().gameObject.name} destroyed");
        }
    }
}





