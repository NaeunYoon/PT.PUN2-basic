using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI _roomName = null;
    [SerializeField] private RoomListMenu _roomListMenu = null;
    private MainCanvases _mainCanvases = null;
     
    /// <summary>
    /// Createroom : 만들어 방을 새로 생성하려고 시도
    /// JoinOrCreateRoom : 룸이 있으면, 참여할 것이고 그렇지 않으면 새로운 룸이 생성
    /// JoinRandomRoom : 룸이 존재하지 않거나 참여할 수 없는 경우(비공개, 가득 참) 들어갈 수 없음
    /// </summary>
    public void OnClickCreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
        {
            return;
        }
        //룸 옵션을 미리 설정해줌
        Debug.Log("OnClick_CreateRoom");
        RoomOptions options = new RoomOptions();
        options.PublishUserId = true;
        options.MaxPlayers = 2;
        options.BroadcastPropsChangeToAll = true;
        PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room",this);
        //룸이 생성되면 현재룸을 활성화시켜줌
        _mainCanvases.currenRoomMenu.gameObject.SetActive(true);
        
    }
    public override void OnCreateRoomFailed(short returnCode, string msg)
    {
        Debug.Log("CreateRoom Failed"+msg,this);
    }

    public void Initialize(MainCanvases canvase)
    {
        _mainCanvases = canvase; 
        _roomListMenu.Initialize(canvase);
    }


    
}
