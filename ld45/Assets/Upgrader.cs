using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    [SerializeField]  private  CellFactory CF;

    public void Awake()
    {
        //Scaling size for 2k, 4k 
        float ScreenScaleX = Camera.main.scaledPixelWidth / 3840.0f   ;
        float ScreenScaleY = Camera.main.scaledPixelHeight / 2160.0f   ;
        //Camera.main.

            //Debug.LogError("ScreenX:" + ScreenScaleX+"Width:"+ Camera.main.aspect);
           // Debug.LogError("ScreenY:" + ScreenScaleY+"Height:"+ Camera.main.aspect);

        transform.localScale = new Vector3(1.2f* ScreenScaleX, 1.2f* ScreenScaleY,1);



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
