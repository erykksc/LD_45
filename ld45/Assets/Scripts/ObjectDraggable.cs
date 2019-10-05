using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDraggable : MonoBehaviour
{
    public bool IsSelected = false;

    private void OnMouseOver()
    {
        //When LMB is pressed, start following
        if (Input.GetMouseButtonDown(0))
        {
            IsSelected = true;
        }
    }


    void Update()
    {
        //While holding LMB, object follows the mouse position
        if (IsSelected) ObjectFollowsMouse(gameObject);

        //When LMB is relesed, (drag ends)  do the following
        if (IsSelected &&Input.GetMouseButtonUp(0) )
        {
            IsSelected = false;
            
        }
        
    }

    public void ObjectFollowsMouse(GameObject ControlledObject)
    {
        Vector2 position = Camera.main.ScreenToWorldPoint(     new Vector2(Input.mousePosition.x, Input.mousePosition.y)    );
        ControlledObject.transform.position = position;
    }
}
