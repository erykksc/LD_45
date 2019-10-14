using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silos : Building
{
    // Start is called before the first frame update
    public override void Upgrade()
    {
        base.Upgrade();
        animationOffset = current.level - 1;
        renderer.sprite = sprites[animationPerUpgrade * (animationOffset)];
        if(getFactory()==null)
        {
            Debug.Log("Major fuck up");
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        base.OnDestroy();
        ((BuildingFactory)getFactory()).updateAvailable();
    }
}
