using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviourPunCallbacks
{
    public bool IsLoading_Enemy = false;



    private void Update()
    {
        Load();
        if (IsLoading_Enemy)
        {
            Destroy(gameObject);
        }
    }

    public void Load()
    {
        photonView.RPC("Set_Enemy_Loading", RpcTarget.Others);
    }

    [PunRPC]
    public void Set_Enemy_Loading()
    {
        IsLoading_Enemy = true;
    }
}