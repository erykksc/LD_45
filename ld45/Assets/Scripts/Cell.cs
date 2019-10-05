using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool IsActive;
        public Sprite SpriteActivated;
        public Sprite SpriteDeactivated;


    public void Start()
    {
        StartCoroutine(LoopActivation());
    }

    public void FixedUpdate()
    {
        //Setting sprite accordingly to cell's activation state
        if (IsActive && gameObject.GetComponent<SpriteRenderer>().sprite == SpriteDeactivated) gameObject.GetComponent<SpriteRenderer>().sprite = SpriteActivated;
        if (!IsActive && gameObject.GetComponent<SpriteRenderer>().sprite == SpriteActivated) gameObject.GetComponent<SpriteRenderer>().sprite = SpriteDeactivated;
    }

    // Update is called once per frame
    IEnumerator LoopActivation()
    {
        while (true)
        {
            IsActive = !IsActive;
            Debug.Log("change");
            yield return new WaitForSeconds(2);
        }

    }
}
