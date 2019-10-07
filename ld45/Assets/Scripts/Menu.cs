using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject ControlsPanel;


    public void PlayButtonPress()
    {
        SceneManager.LoadScene(1);
    }

    public void ControlsButtonPress()
    {
        ControlsPanel.active = true;
    }

    public void CloseControlButtonPress()
    {
        ControlsPanel.active = false;
    }

}
