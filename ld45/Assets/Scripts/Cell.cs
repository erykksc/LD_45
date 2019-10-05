using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    // Start is called before the first frame update

    public Cell right = null , up = null , down = null, left = null;
    
    public void Instantiate(Vector2Int p)
    {
        pos = p;
        transform.localPosition = (Vector2) pos;
    }

    public bool active = true;

    public bool isActivated = false;

    public int timesActivated = 0;

    public SpriteRenderer renderer;

    public Vector2Int pos;

    [SerializeField] private int hp = 100;

    public int getHp()
    {
        return hp;
    }
    void setHp(int h)
    {
        hp = h;
    }
    /// Functions
    /*public IEnumerator animate(int duration)
    {
        Color color = new Color(1, 1, 1);
        float t = 0;
        while(t<duration)
        {
            color.r = 1 * (t / duration);
            t += Time.deltaTime;
            yield return null;
        }
    }*/

    public void getImpulse(Cell parent)
    {
        Debug.Log("from getImpulse");
        if(parent.timesActivated>timesActivated)
        {
            //Debug.Log("got impulse");
            isActivated = true;
            timesActivated = parent.timesActivated;
            StartCoroutine(propagateImpuls());
        }
    }

    // coroutine
    public IEnumerator propagateImpuls()
    {
        renderer.color = Color.green;
        Debug.Log($"inside propagate impulse, activated{timesActivated}");
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
        isActivated = false;
        renderer.color = Color.blue;
    }

    public void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = Color.blue;
        //Debug.Log("change color");
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
