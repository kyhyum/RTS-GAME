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

    private void Start()
    {
        DontDestroyOnLoad(gameObject);    
    }

    private void Update()
    {
        Load();
        if (IsLoading_Enemy)
        {
            Destroy(gameObject);
        }
    }

    public override void OnJoinedRoom()
    {
        // 현재 씬 이름이 "Lobby"인 경우에만 LoadLevel() 함수 호출
        if (SceneManager.GetActiveScene().name == "BattleScene")
        {
            Load();
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
