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

    void Upgrade()
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

    Terrain terrainUnder;

    public void setTerrain(Terrain u)
    {
        terrainUnder = u;
    }

    protected void onPulse()
    {
        StartCoroutine(animatePulse());
        current.hp += current.selfHeal;
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

    bool receiveDamage(int damage)
    {
        current.hp -= damage;
        if(current.hp<=0)
        {
            return true;
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
