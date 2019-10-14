using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Building
{
    // Start is called before the first frame update
    [SerializeField] SpriteRenderer head;
    [SerializeField] float turningTime;
    [SerializeField] Sprite[] heads;

    IEnumerator Seek(GameObject target)
    {
        Vector2 dir1 = target.transform.position-transform.position;
        Vector2 dir2 = head.transform.right;
        Vector2 dir3;
        float time = 0;
        while(time<turningTime)
        {
            head.transform.right = Vector3.Normalize((dir1*(turningTime-time) + dir2*time)/turningTime);
            time += Time.deltaTime;
            yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }
    public override void Upgrade()
    {
        
        base.Upgrade();
        if (head != null&&current.level-1<3)
        {
            head.sprite = heads[current.level - 1];
        }
    }

    private void Awake()
    {
        base.Awake();
        head = GetComponentsInChildren<SpriteRenderer>()[1];
        head.transform.localPosition = new Vector2(0, 0);

        current.level = 0;
        Upgrade();
        head.sprite = heads[current.level - 1];
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
