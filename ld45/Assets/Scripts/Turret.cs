using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Building
{
    // Start is called before the first frame update
    [SerializeField] SpriteRenderer head;
    [SerializeField] float turningTime;
    [SerializeField] Sprite[] heads;
    EnemyFactory eFactory;
    bool shooting = false;

    void Seek()
    {
        Enemy t;
            t = eFactory.getClosestTo(transform.localPosition);
            if (t == null)
            {
            return;
            }
            if(Vector2.Distance(transform.localPosition,t.transform.localPosition)>current.range)
            {
            return;
            }
            Debug.Log("Enemy Found");
            StartCoroutine(DestroyEnemy(t));
        
    }
    IEnumerator DestroyEnemy(Enemy target)
    {
        
        Vector2 dir1 = -Vector3.Normalize(target.transform.position-transform.position);
        Vector2 dir2 = head.transform.right;
        Vector2 dir3;
        float time = 0;
        while(time<turningTime)
        {
            dir1 = -Vector3.Normalize(target.transform.position - transform.position);
            head.transform.right = (dir1 * time + dir2 * (turningTime - time))/turningTime;
            time += Time.deltaTime;
            yield return new WaitForSeconds(0.05f);
        }
        //head.transform.right = Vector2.down;
        //head.transform.right = dir1;
        target.receiveDamage(current.damage);
        Debug.Log("Enemy shot");
        yield return null;
    }
    public override void Upgrade()
    {
        
        base.Upgrade();
        if (head != null&&current.level<4)
        {
            head.sprite = heads[current.level - 1];
        }
    }

    protected override void onPulse()
    {
        base.onPulse();
        Seek();
        Debug.Log("Overriden");
    }

    private void Awake()
    {
        base.Awake();
        head = GetComponentsInChildren<SpriteRenderer>()[1];
        head.transform.localPosition = new Vector2(0, 0);

        current.level = 0;
        Upgrade();
        head.sprite = heads[current.level - 1];
        eFactory = EnemyFactory.getFactory();
        turningTime = 0.2f;
    }

    IEnumerator animateBullet()
    {
        yield return null;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
