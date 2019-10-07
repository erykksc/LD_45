using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMeAfterTime : MonoBehaviour
{
    [SerializeField] private float TimeLeft;


    private void FixedUpdate()
    {
        TimeLeft -= Time.deltaTime;

        if (TimeLeft == 0) Destroy(gameObject);
    }
}
