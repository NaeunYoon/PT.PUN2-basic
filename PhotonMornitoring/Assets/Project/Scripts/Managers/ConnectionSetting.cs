using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

//MonoBehaviourPunCallbacks 상속
public class ConnectionSetting : MonoBehaviourPunCallbacks
{
    public string gameVersion = "0.0.0";
    [SerializeField] private GameObject message;
    private TextMeshProUGUI txt = null;
    void Start()
    {
        message.gameObject.SetActive(true);
        txt = message.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        txt.text = "Connecting to Photon Server";
        Debug.Log("Connecting to Photon Server");
        //자동으로 씬 동기화 하는 함수 (플레이어 간 장면로드)
        PhotonNetwork.AutomaticallySyncScene = true;
        //게임 버전 설정
        //userInfo에서 닉네임 새로 설정
        //PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        //포톤 클라우드 마스터서버에 제일 먼저 연결한다.
        PhotonNetwork.ConnectUsingSettings();
        //마스터에 연결한 사용자에게 가장 적합한 핑 서버를 연결한다.
        //PhotonNetwork.ConnectToBestCloudServer();
    }
    /// <summary>
    /// 마스터 서버 접속 성공 시 자동 실행하는 함수
    /// 로비에 접속하게 함
    /// </summary>
    public override void OnConnectedToMaster()
    {
        txt.text = "Connected to Photon Server";
        Debug.Log("Connected to Photon Server"+ PhotonNetwork.LocalPlayer.NickName);
        Debug.Log("PhotonNetwork.InLobby "+PhotonNetwork.InLobby);
        
        if (!PhotonNetwork.InLobby)
        {
            //로비진입
            PhotonNetwork.JoinLobby();
            Debug.Log(PhotonNetwork.LocalPlayer.NickName +" joined Lobby");
            message.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 마스터서버 접속 실패 시 자동 실행
    /// </summary>
    /// <param name="disconnectCause"></param>
    public override void OnDisconnected(DisconnectCause disconnectCause)
    {
        Debug.Log("DisConnected from Photon Server for reason : " + disconnectCause);
    }
   
}
