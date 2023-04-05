using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Loading : MonoBehaviourPunCallbacks
{
    private bool IsLoading_Enemy = false;

    private void Start()
    {
        photonView.RPC("is_Connect", RpcTarget.Others);
    }
    // Update is called once per frame
    void Update()
    {
        if(!IsLoading_Enemy)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    [PunRPC]
    public void is_Connect()
    {
        IsLoading_Enemy = true;
    }
}
