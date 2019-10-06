using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSDestroyer : MonoBehaviour
{

    //When the particle emmiter has no particles it gets destroyed
    void FixedUpdate()
    {
        if (gameObject.GetComponent<ParticleSystem>().particleCount == 0) Destroy(gameObject);

    }
}
