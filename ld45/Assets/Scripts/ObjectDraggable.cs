using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDraggable : MonoBehaviour
{
    public bool IsSelected = false;
    public CellFactory Factory;
    public CellFactory grassFactory;
    public int SpawnedIdentifier=0;
    public Vector3 ReturnPosition;
    public bool makeInfinite = false;

    public void SetDraggableReturnPosition()
    {
        ReturnPosition = gameObject.transform.localPosition;
    }

    bool switch1 = true;

    private void OnMouseOver()
    {
        //When LMB is pressed, start following
        if (Input.GetMouseButtonDown(0))
        {
            SetDraggableReturnPosition();
            IsSelected = true;
        }
    }


    
    void Update()
    {
        if(switch1)
        {
            switch1 = false;
            CellFactory.cellCount = 0;
        }

        //While holding LMB, object follows the mouse position
        if (IsSelected) ObjectFollowsMouse(gameObject);

        //When LMB is relesed, (drag ends)  do the following
        if (IsSelected &&Input.GetMouseButtonUp(0) )
        {
            
            IsSelected = false;
            Vector2 WorldPos =  Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));

            Vector2Int hPos = Cell.getHexCoords(WorldPos, 55f/64f);

            
                
            //Checking if position is occupied and if player has enough cash to build the cell
            if ((Factory.Find(hPos) == null&&ScoreCore.Cash>= ScoreCore.Prices[SpawnedIdentifier]&&grassFactory.Find(hPos).buildable) && Input.mousePosition.y>Camera.main.pixelWidth/7&&Silos.getAvailableBuildings()>CellFactory.cellCount  ) // > (Camera.main.pixelHeight/10) 
            {
                CellFactory.cellCount++;
                Factory.Add(hPos, SpawnedIdentifier);
                GameObject.Instantiate(Resources.Load<GameObject>("BuildParticles") as GameObject, Cell.getGlobalCoords(Cell.getHexCoords(WorldPos, 55f/64f), 55f/64f), Quaternion.identity);
                ScoreCore.Cash -= ScoreCore.Prices[SpawnedIdentifier];
                ScoreCore.Prices[SpawnedIdentifier] += 5;
                Camera.main.GetComponent<ScoreCore>().PriceDisplayers[SpawnedIdentifier].text = ScoreCore.Prices[SpawnedIdentifier].ToString() + "$";
                /*
                if (switch1)
                {
                    switch1 = false;
                    CellFactory.cellCount = 1;
                }
                */
                Debug.Log($"Info cell: {CellFactory.cellCount}");
            }

            //Increase Price of thebuilding built
            


            //Returning the draggable to origin position
            gameObject.transform.localPosition = ReturnPosition;
        }
        
    }
    public void ObjectFollowsMouse(GameObject ControlledObject)
    {
        Vector2 position = Camera.main.ScreenToWorldPoint(     new Vector2(Input.mousePosition.x, Input.mousePosition.y)    );
        ControlledObject.transform.position = position;
    }


}
