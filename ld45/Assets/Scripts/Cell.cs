using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    // Start is called before the first frame update

    public Cell right = null , up = null , down = null, left = null;

    public void Awake()
    {
    //    SnapToIntPosition();
    }
    
    public bool isActivated = false;

    public int timesActivated = 0;

    public Vector2Int pos;

    public void getImpulse(Cell parent)
    {
        if(parent.timesActivated>timesActivated)
        {
            isActivated = true;
            timesActivated = parent.timesActivated;
            StartCoroutine(propagateImpuls());
        }
    }

    // coroutine
    private IEnumerator propagateImpuls()
    {
        yield return new WaitForSeconds(0.5f);
        if(right!=null)
        {
            right.getImpulse(this);
        }
        if (left != null)
        {
            left.getImpulse(this);
        }
        if (up != null)
        {
            up.getImpulse(this);
        }
        if (down != null)
        {
            down.getImpulse(this);
        }
    }

    void Start()
    {
        
    }

    private void Update()
    {

    }

    // Update is called once per frame

    public void FixedUpdate()
    {
        //Setting sprite accordingly to cell's activation state
        //if (isActivated && gameObject.GetComponent<SpriteRenderer>().sprite == SpriteDeactivated) gameObject.GetComponent<SpriteRenderer>().sprite = SpriteActivated;
        //if (!isActivated && gameObject.GetComponent<SpriteRenderer>().sprite == SpriteActivated) gameObject.GetComponent<SpriteRenderer>().sprite = SpriteDeactivated;
    }

    //This will ensure that this GameObject is at coordinates expressed in Int values
    public void SnapToIntPosition()
    {
        Vector2 SnappedPosition = new Vector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
        transform.position = SnappedPosition;
    }

}
