using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Zoom : MonoBehaviour
{
    private int currentZoom = 0;
    [SerializeField] private Camera mainCamera;
    private PixelPerfectCamera pPCamera;
    private int startingX;
    private int startingY;
    private int aspect;

    private void Start() {
        pPCamera = mainCamera.GetComponent<PixelPerfectCamera>();
        startingX = pPCamera.refResolutionX;
        startingY = pPCamera.refResolutionY;
    }

    private int getAspectRatio()
    {
        float widthTHeight = mainCamera.aspect;

        if (widthTHeight == 16f/9f)
        {
            //16:9
            return 16;
        }
        else if (widthTHeight == 4f/3f)
        {
            //4;3
            return 4;
        }
        else if (widthTHeight == 21)
        {
            //21:9
            return 21;
        }
        else
        {
            //not recognized
            return 0;
        }
    }

    private void changeZoomValue()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if ((scrollWheel < 0.0f) || Input.GetKey(KeyCode.KeypadMinus))
        {
            // scroll down
            if (currentZoom < 50)
            {
                currentZoom += 1;
            }
        }
        else if ((scrollWheel > 0.0f) || Input.GetKey(KeyCode.LeftArrow))
        {
            //scroll up
            if (currentZoom > 0)
            {
                currentZoom -= 1;
            }
        }
    }

    private void updateZoomFromValue()
    {
        pPCamera.refResolutionX = startingX + currentZoom*aspect*2;
        // pPCamera.refResolutionY = startingY + currentZoom*18;
    }

    private void FixedUpdate()
    {
        aspect = getAspectRatio();
        changeZoomValue();
        updateZoomFromValue();
    }
}