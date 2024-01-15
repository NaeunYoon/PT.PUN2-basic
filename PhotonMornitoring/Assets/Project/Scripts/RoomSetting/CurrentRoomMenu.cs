using Photon.Pun;
using UnityEngine;

public class CurrentRoomMenu : MonoBehaviourPunCallbacks
{
    private MainCanvases _mainCanvases = null;
    [SerializeField] private PlayerListMenu _playerListMenu;
    
    public void Initialize(MainCanvases canvase)
    {
        _mainCanvases = canvase;
        _playerListMenu.Initialize(canvase);
    }
    
    public void Onclick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        _mainCanvases.currenRoomMenu.gameObject.SetActive(false);
    }
  
}
