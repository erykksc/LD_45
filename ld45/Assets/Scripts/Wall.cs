using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Cell
{
    [SerializeField] private int healPerImpulse = 10;
    public int getHealPerImpulse()
    {
        return healPerImpulse;
    }
    public void setHealPerImpulse(int newHealPerImpulse)
    {
        healPerImpulse = newHealPerImpulse;
    }
    void onImpulse()
    {
        dealDamage(-getHealPerImpulse());
    }
    
    private void Awake()
    {
        setPulseAction(action);
        //GetComponent<SpriteRenderer>().color = Color.green;
        //sprite = gameObject.GetComponent<Sprite>();
    }
    
    //public override void WhenActivatedDoOnce()
    //{
        //setHp(getHp()+healPerImpulse);
    //}
}
