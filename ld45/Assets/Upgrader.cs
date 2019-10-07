using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    [SerializeField]  private  CellFactory CF;

    public void Awake()
    {
        CF = null;
        Transform SemiTarget = null;
        
        //Getting access to the CellFactory without .Find
        foreach (Transform trans in Camera.main.GetComponentsInChildren<Transform>())
        {
            if (trans.gameObject.name == "Canvas") SemiTarget = trans;
        }
        foreach (Transform trans in Camera.main.GetComponentsInChildren<Transform>())
        {
            if (trans.gameObject.name == "CellFactory") SemiTarget = trans;
        }
        CF = SemiTarget.GetComponent<CellFactory>();



    }

    public void Upgrade()
    {
        CF.Find(Cell.getHexCoords(transform.position, 55f / 64f)).Upgrade();
    }


    public void Bulldoze()
    {
        CF.DestroyCell(Cell.getHexCoords(transform.position, 55f / 64f)    );    
    }
    private void FixedUpdate()
    {
        if (CF.Find(Cell.getHexCoords(transform.position, 55f / 64f))==null) Destroy(gameObject);
    }
}
