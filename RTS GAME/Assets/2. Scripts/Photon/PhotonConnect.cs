using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;

public class PhotonConnect : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1.0";

    public Button joingButton;
    public Button CancelButton;
    public GameObject matching_popup;
    // Start is called before the first frame update

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        PhotonNetwork.ConnectUsingSettings();

        CancelButton.interactable = false;
        joingButton.interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    // ���� ���� ���� ��
    public override void OnConnectedToMaster()
    {
        joingButton.interactable = true;
    }

    // ������ ������ ������ ��
    public override void OnDisconnected(DisconnectCause cause)
    {
        joingButton.interactable = false;
        PhotonNetwork.ConnectUsingSettings();

    }
    
    // ��Ī ��ư Ŭ�� �Լ�
    public void Matching()
    {
        joingButton.interactable = false;
        //������ ���� ���̸� �뿡 ���� �õ�
        if (PhotonNetwork.IsConnected)
        {
            matching_popup.SetActive(true);
            PhotonNetwork.JoinRandomRoom(); 
        }
        // ������ ����Ǿ� ���� �ʴٸ� ���� �翬��
        else
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // ��Ī ��� ��ư Ŭ�� �Լ�
    public void MatchingCancel()
    {
        PhotonNetwork.LeaveRoom();
        matching_popup.SetActive(false);
        CancelButton.interactable = false;
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("�� ���� �Ϸ�");
       CancelButton.interactable = true;
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if(PhotonNetwork.CurrentRoom.PlayerCount == 2)
            {
                PhotonNetwork.LoadLevel("MatchingMenu");
            }
        }
    }
}
