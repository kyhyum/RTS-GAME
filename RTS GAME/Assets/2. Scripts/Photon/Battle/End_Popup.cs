using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_Popup : MonoBehaviourPunCallbacks
{

    public GameObject Lose_Text;
    public GameObject Win_Text;

    void Start()
    {
        Lose_Text.SetActive(false);
        Win_Text.SetActive(false);
        this.gameObject.SetActive(false);
    }

    public void Active_End_Popup(bool iswin)
    {
        this.gameObject.SetActive(true);
        if(iswin)
        {
            Win_Text.SetActive(true);
        }
        else
        {
            Lose_Text.SetActive(true);
        }
    }

    public void ExitButton()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainMenu");
    }
}
