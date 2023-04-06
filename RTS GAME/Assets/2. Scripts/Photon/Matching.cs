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
    // 내 종족, 상대 종족 TEXT
    public TMP_Text me_tribe;
    public TMP_Text opp_tribe;
    // 준비 버튼
    public Button READY_btn;
    private Color color;

    public TMP_Dropdown dropdown;

    private int Timer = 50;
    public TMP_Text time;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        color = me.color;
        StartCoroutine("Count");
    }

    // Update is called once per frame
    void Update()
    {
        // 카운트 다운 표시
        time.text = Timer.ToString();

        // 준비 완료 또는 준비 중 표시
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

        // 모두 준비 완료 시 5초 카운트 다운
        if (me_ready && opponent_ready)
        {
            READY_btn.interactable = false;
            if (Timer > 5)
                Timer = 5;
        }

        photonView.RPC("opponent_tribe", RpcTarget.Others, me_tribe.text.ToString());
    }
    IEnumerator Count()
    {
        while (Timer > 0)
        {
            Debug.Log(Timer);
            yield return new WaitForSeconds(1.0f);
            Timer--;
        }
        //게임시작
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
            photonView.RPC("opp_ready", RpcTarget.Others, false);
        }
        else
        {
            me_ready = true;
            dropdown.interactable = false;
            photonView.RPC("opp_ready", RpcTarget.Others, true);
        }
    }

    [PunRPC]
    public void opponent_tribe(string tribe)
    {
        opp_tribe.text = tribe;
    }

    [PunRPC]
    public void opp_ready(bool state)
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