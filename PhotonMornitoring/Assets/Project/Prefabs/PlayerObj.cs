using Photon.Realtime;
using TMPro;
using UnityEngine;

public class PlayerObj : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text = null;

    public Player _player = null;
    public bool isready = false;
    /// <summary>
    /// 접속한 플레이어를 생성할 때 할당해줌
    /// </summary>
    /// <param name="playerInfo"></param>
    public void SetPlayerInfo(Player playerInfo)
    {
        _player = playerInfo;
        _text.text = playerInfo.NickName +" / "+ playerInfo.UserId;
    }
}


