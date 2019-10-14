using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    [System.Serializable] protected struct Status
    {
        public float hp;
        public float damage;
        public float speed;
    }

    [SerializeField] Status current;

    [SerializeField] TerrainFactory tFactory;
    [SerializeField] EnemyFactory factory;

    SpriteRenderer renderer;
    [SerializeField] Sprite[] sprites;

    Color color;

    public Vector2 pos;
    public Vector2 vel;

    [SerializeField]Vector2 dest;

    Cell dstCell;

    int dValue = int.MaxValue;

    IEnumerator walkAnimation()
    {
        yield return null;
    }

    void pickDest()
    {
        Terrain t = (Terrain) tFactory.Find(Cell.getHexCoords(transform.localPosition, 55f / 64f));
        if(t==null)
        {
            return;
        }
        Debug.Log($"Localized on:{t.pos}");
        for(int i = 0;i<6;i++)
        {
            if(t.getNeighbour(i)!=null)
            {
                if(((Terrain)t.getNeighbour(i)).distToGen<dValue)
                {
                    dstCell = t.getNeighbour(i);
                    dest = (t.getNeighbour(i).transform.localPosition);
                    dValue = ((Terrain)t.getNeighbour(i)).distToGen;
                }
            }
        }
    }

    void Start()
    {
        
    }

    private void Awake()
    {
        
        renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = sprites[0];
        color = Color.red;
        renderer.color = color;
    }
    public void Instantiate(Vector2 p,TerrainFactory tf,EnemyFactory ef)
    {
        pos = p;
        tFactory = tf;
        factory = ef;
        
        factory.AddToList(this);
        vel = new Vector2(0, 0);
        transform.localPosition = pos;
        StartCoroutine(Guidance());
    }

    bool receiveDamage(float damage)
    {
        current.hp -= damage;
        if(current.hp<=0)
        {
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    // Update is called once per frame
    IEnumerator Guidance()
    {
        yield return new WaitForSeconds(Random.Range(0, 750) / 1000f);
        while(true)
        {
            pickDest();
            vel = Vector3.Normalize(dstCell.getLocalPos() - ((Vector2)transform.localPosition))*current.speed ;
            yield return new WaitForSeconds(0.75f);
        }
    }

    void FixedUpdate()
    {
        transform.localPosition += (Vector3)vel * Time.fixedDeltaTime;
    }
    private void OnDestroy()
    {
        factory.removeFromList(this);
    }
}
