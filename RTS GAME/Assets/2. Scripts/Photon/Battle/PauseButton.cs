using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public End_Popup endPopup;
    public GameObject settingPopup;

    // Start is called before the first frame update
    void Start()
    {
        settingPopup.SetActive(false);
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
        endPopup.ActiveEndPopup(false);
    }

    public void SetActive_Setting_popup()
    {
        settingPopup.SetActive(true);
    }

    public void SetUnActive_Setting_popup()
    {
        settingPopup.SetActive(false);
    }
}
