using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class RoomObj : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text = null;

    public RoomInfo RoomInfo { get; private set; }

    /// <summary>
    /// 룸이 생성된 후, 룸인포 리스트에 담긴 정보를 텍스트로 출력
    /// </summary>
    /// <param name="roomInfo"></param>
    public void SetRoomInfo(RoomInfo roomInfo)
    {
        //생성될때 할당.
        RoomInfo = roomInfo;
        _text.text = roomInfo.MaxPlayers +" / "+ roomInfo.Name;
    }
    /// <summary>
    ///  객체에 할당된 룸 정보를 바탕으로 접속한다
    /// </summary>
    public void Onclick_Btn()
    {
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }
}


