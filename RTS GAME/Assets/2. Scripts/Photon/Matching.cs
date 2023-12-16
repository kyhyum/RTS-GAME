using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Diagnostics.Tracing;
using UnityEngine.SceneManagement;

public class Matching : MonoBehaviourPunCallbacks
{
    public bool me_ready = false, opponent_ready = false;


    // READY TEXT
    public TMP_Text me, opponent;
    // �� ����, ��� ���� TEXT
    public TMP_Text me_tribe;
    public TMP_Text opp_tribe;
    // �غ� ��ư
    public Button READY_btn;
    private Color color;

    public TMP_Dropdown dropdown;

    private int Timer = 50;
    public TMP_Text time;

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        color = me.color;
        StartCoroutine("Count");
    }

    void Update()
    {
        // ī��Ʈ �ٿ� ǥ��
        time.text = Timer.ToString();

        if (Timer > 0)
        {
            // �غ� �Ϸ� �Ǵ� �غ� �� ǥ��
            if (me_ready)
            {
                color.a = 1f;
                me.color = color;
            }
            else
            {
                color.a = 0f;
                me.color = color;
            }
            if (opponent_ready)
            {
                color.a = 1f;
                opponent.color = color;
            }
            else
            {
                color.a = 0f;
                opponent.color = color;
            }
            photonView.RPC("opponent_tribe", RpcTarget.Others, me_tribe.text.ToString());
        }

        // ��� �غ� �Ϸ� �� 5�� ī��Ʈ �ٿ�
        if (me_ready && opponent_ready)
        {
            READY_btn.interactable = false;
            if (Timer > 5)
                Timer = 5;
        }

    }
    IEnumerator Count()
    {
        while (Timer > 0)
        {
            Debug.Log(Timer);
            yield return new WaitForSeconds(1.0f);
            Timer--;
        }
        //���ӽ���
        if (Timer == 0)
        {
            PlayerPrefs.SetString("Enemy_Tribe", opp_tribe.text.ToString());
            PlayerPrefs.SetString("Player_Tribe", me_tribe.text.ToString());
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("BattleScene");
            }
        }

    }

    public void Ready()
    {
        if (me_ready)
        {
            me_ready = false;
            dropdown.interactable = true;
            photonView.RPC("ready", RpcTarget.Others, false);
        }
        else
        {
            me_ready = true;
            dropdown.interactable = false;
            photonView.RPC("ready", RpcTarget.Others, true);
        }
    }

    [PunRPC]
    public void OpponentTribe(string tribe)
    {
        opp_tribe.text = tribe;
    }

    [PunRPC]
    public void Ready(bool state)
    {
        opponent_ready = state;
    }

    public void LeaveRoom()
    {
        Leave();
        photonView.RPC("Leave", RpcTarget.Others);
    }

    [PunRPC]
    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("MainMenu");
    }
    
}