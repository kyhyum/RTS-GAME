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

    public void ActiveEndPopup(bool iswin)
    {
        this.gameObject.SetActive(true);
        if(iswin)
        {
            Win_Text.SetActive(true);
            photonView.RPC("Active_Popup", RpcTarget.Others, false);
        }
        else
        {
            Lose_Text.SetActive(true);
            photonView.RPC("Active_Popup", RpcTarget.Others, true);
        }
    }

    [PunRPC]
    public void ActivePopup(bool iswin)
    {
        this.gameObject.SetActive(true);
        if (iswin)
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
