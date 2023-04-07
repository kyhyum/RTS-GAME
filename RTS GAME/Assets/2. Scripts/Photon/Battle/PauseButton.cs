using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public End_Popup end_popup;
    public GameObject Setting_Popup;

    // Start is called before the first frame update
    void Start()
    {
        Setting_Popup.SetActive(false);
        this.gameObject.SetActive(false);   
    }

    public void SetActive_Pause_popup()
    {
        this.gameObject.SetActive(true);
    }

    public void SetUnActive_Pause_popup()
    {
        this.gameObject.SetActive(false);
    }

    public void Surrender()
    {
        end_popup.Active_End_Popup(false);
    }

    public void SetActive_Setting_popup()
    {
        Setting_Popup.SetActive(true);
    }

    public void SetUnActive_Setting_popup()
    {
        Setting_Popup.SetActive(false);
    }
}
