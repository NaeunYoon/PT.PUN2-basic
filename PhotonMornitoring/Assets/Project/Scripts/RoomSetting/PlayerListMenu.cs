using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayerListMenu : MonoBehaviourPunCallbacks
{
     [Header("PlayerListing")]
    [SerializeField] private Transform _contents;
    [SerializeField] private PlayerObj _playerObj;
    [Space(5f)]
    [Header("StartGame")]
    [SerializeField] private TextMeshProUGUI _readyUpText;
    private bool _ready = false;
    [Space(5f)]
    [Header("Etc")]
    private List<PlayerObj> _list = new List<PlayerObj>();
    private MainCanvases _canvases;

    public override void OnEnable()
    {
        //SetReadyUp(false);
        base.OnEnable();
        OnStartReady(false);
        GetCurrentRoomPlayers();
    }

    private void OnStartReady(bool state)
    {
        _ready = state;
        
        if (_ready)
            _readyUpText.text = "Y";
        else
            _readyUpText.text = "N";
    }
    
    public override void OnDisable()
    {
        base.OnDisable();
        for (int i = 0; i < _list.Count; i++)
        {
            Destroy(_list[i].gameObject);
        }
        _list.Clear();
    }
    
    public void Initialize(MainCanvases canvases)
    {
        _canvases = canvases;
    }
    /// <summary>
    /// 포톤이 네트워크에 연결되어있는지 확인
    /// </summary>
    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            return;
        foreach (KeyValuePair<int,Player> item in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(item.Value);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }
    
    public override void OnPlayerLeftRoom(Player newPlayer)
    {
        //base.OnPlayerEnteredRoom(newPlayer);
        int index = _list.FindIndex(x => x._player == newPlayer);
        Debug.Log("LEFT");
        if (index != -1)
        {
            Destroy(_list[index].gameObject);
            _list.RemoveAt(index);
        }
    }
    private void AddPlayerListing(Player player)
    {
        int index = _list.FindIndex(x => x._player == player);
        if (index != -1)
        {
            //플레이어 정보 업데이트
            _list[index].SetPlayerInfo(player);
        }
        else
        {
            //새로 생성
            PlayerObj listing = Instantiate(_playerObj, _contents);
            Debug.Log("ENTER");
            if (listing != null)
            {
                listing.SetPlayerInfo(player);
                _list.Add(listing);
            }
        }
    }
    public override void OnMasterClientSwitched(Player player)
    {
        base.OnMasterClientSwitched(player);
        _canvases.currenRoomMenu.Onclick_LeaveRoom();
    }

    
    /// <summary>
    /// master client 가 버튼을 누르면 시작한다.
    /// </summary>
    public void OnclickeStartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i]._player != PhotonNetwork.LocalPlayer)
                {
                    if(!_list[i].isready)
                        return;
                }
            }
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            //빌드인덱스
            PhotonNetwork.LoadLevel(1);
            
        }
    }
    /// <summary>
    /// master가 아닌 clients들을 준비시킴.
    /// </summary>
    public void Onclick_Ready()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            OnStartReady(!_ready);
            base.photonView.RPC("RPC_ChangeReadyState",RpcTarget.MasterClient,PhotonNetwork.LocalPlayer,_ready);
            //base.photonView.RpcSecure("RPC_ChangeReadyState",RpcTarget.MasterClient,true,PhotonNetwork.LocalPlayer,_ready);
            //PhotonNetwork.RemoveRPCs();
        }
    }

    /// <summary>
    /// 플레이어들의 준비사항을 확인함
    /// </summary>
    /// <param name="player"></param>
    /// <param name="ready"></param>
    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool ready)
    {
        Debug.Log("Ready"+_ready);
        int index = _list.FindIndex(x => x._player == player);
        if (index != -1)
            _list[index].isready = ready;
    }
}
