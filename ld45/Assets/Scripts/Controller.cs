using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    float Cash;

    float buildingCount;

    float buildingsAvailable;

    Vector2 cpmax,cpmin;

    [SerializeField] Camera cam;
    [SerializeField] TerrainFactory tFactory;
    [SerializeField] BuildingFactory bFactory;

    private void Awake()
    {
        if (tFactory == null||bFactory == null||cam==null)
        {
            Debug.Log("In Controller, Awake: Lacking factory object");
        }
        tFactory.GenerateMap();
        bFactory.Initialize();

        Vector2 ssize;
        ssize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10f));
        

        cpmax = Cell.getGlobalCoords(tFactory.getSize(), 55f / 64f)-ssize;
        cpmin = new Vector2(0, 0)+ssize;
        cam.transform.localPosition = (cpmin+cpmax)/ 2;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            cam.transform.localPosition += new Vector3(-5, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            cam.transform.localPosition += new Vector3(0, 5, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            cam.transform.localPosition += new Vector3(0, -5, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            cam.transform.localPosition += new Vector3(5, 0, 0) * Time.deltaTime;
        }
        Vector2 cPos = cam.transform.localPosition;
        if (cPos.x<cpmin.x)
        {
            cPos.x = cpmin.x;
        }
        if (cPos.y < cpmin.y)
        {
            cPos.y = cpmin.y;
        }
        if (cPos.x > cpmax.x)
        {
            cPos.x = cpmax.x;
        }
        if (cPos.y > cpmax.y)
        {
            cPos.y = cpmax.y;
        }
        cam.transform.localPosition = cPos;


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 10f;
            pos = Camera.main.ScreenToWorldPoint(pos);

            bFactory.Build(Cell.getHexCoords(pos,55f/64f), 1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 10f;
            pos = Camera.main.ScreenToWorldPoint(pos);

            bFactory.Build(Cell.getHexCoords(pos, 55f / 64f), 2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 10f;
            pos = Camera.main.ScreenToWorldPoint(pos);

            bFactory.Build(Cell.getHexCoords(pos, 55f / 64f), 3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 10f;
            pos = Camera.main.ScreenToWorldPoint(pos);

            bFactory.Build(Cell.getHexCoords(pos, 55f / 64f), 4);
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 10f;
            pos = Camera.main.ScreenToWorldPoint(pos);
            Building cell = (Building)bFactory.Find(Cell.getHexCoords(pos, 55f / 64f));
            if (cell != null)
            {
                cell.Upgrade();
            }

        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Vector3 pos = Input.mousePosition;
            pos.z = 10f;
            pos = Camera.main.ScreenToWorldPoint(pos);
            Cell cell = bFactory.Find(Cell.getHexCoords(pos, 55f / 64f));
            if(cell!=null)
            {
                Destroy(cell.gameObject);
            }
            
        }
    }
}
