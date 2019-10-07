using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public MapGenerator MapGenerator;
    public float MoveSpeed;

    private float XMaxLimit;
    private float XMinLimit;
    private float YMaxLimit;
    private float YMinLimit;



    public void Start()
    {
        MapGenerator = gameObject.GetComponent<MapGenerator>();
        XMaxLimit = GetComponent<MapGenerator>().xSize * 0.84252352941176470588235294117647f - 7f;
        XMinLimit = 7.5f;
        YMaxLimit = GetComponent<MapGenerator>().ySize * 0.72023225806451612903225806451613f - 4.5f;
        YMinLimit = 5f;
    }

    void FixedUpdate()
    {
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && transform.position.x > XMinLimit)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * MoveSpeed);
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && transform.position.x < XMaxLimit)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * MoveSpeed);
        }
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && transform.position.y < YMaxLimit)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * MoveSpeed);
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && transform.position.y > YMinLimit)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.down * MoveSpeed);
        }

        if (Input.GetKeyDown("g"))
        {
            Vector2 pos = Cell.getGlobalCoords(new Vector2Int((int)((float)MapGenerator.xSize / 2), (int)((float)MapGenerator.ySize / 2)), 55f / 64f);
            transform.position = new Vector3(pos.x, pos.y, -3);
        }
    }
}
