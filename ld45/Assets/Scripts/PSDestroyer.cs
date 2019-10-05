using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSDestroyer : MonoBehaviour
{


    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.GetComponent<ParticleSystem>().particleCount == 0) Destroy(gameObject);

    }
}
