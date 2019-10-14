using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Building : Propagateable
{
    // Start is called before the first frame update
    [System.Serializable]
    protected struct Properties
    {
        public int level;

        public float hp;

        public float moneyps;

        public float damage;

        public float range;

        public float selfHeal;

        public float pulseFrequency;

        public int availableBuildings;
    }

    [SerializeField] protected Properties[] levels;

    [SerializeField] protected Properties current;

    public virtual void Upgrade()
    {
        current.level++;
        if(current.level>3)
        {
            current.level = 3;
            return;
        }

        current = levels[current.level-1];
    }

    void Start()
    {
        
    }

    [SerializeField] protected Cell terrainUnder;

    public void setTerrain(Cell u)
    {
        terrainUnder = u;
    }

    protected void onPulse()
    {
        StartCoroutine(animatePulse());
        receiveDamage(-current.selfHeal);
        ScoreCore.Cash += current.moneyps;
        StartCoroutine(animatePulse());
    }
    

    new protected void Awake()
    {
        base.Awake();
        current.level = 0;
        Upgrade();
        setPulseAction(onPulse);
        
    }

    protected bool receiveDamage(float damage)
    {
        current.hp -= damage;
        if(current.hp<=0)
        {
            return true;
        }
        if(current.hp>levels[current.level-1].hp)
        {
            current.hp = levels[current.level-1].hp;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
