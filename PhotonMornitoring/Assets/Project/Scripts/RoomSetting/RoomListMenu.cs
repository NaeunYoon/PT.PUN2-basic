using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class RoomListMenu : MonoBehaviourPunCallbacks
{
    //방 목록
    [SerializeField] private Transform _contents = null;
    //방 프리팹
    [SerializeField] private RoomObj _prefab = null;

    [SerializeField] private List<RoomObj> _objList = new List<RoomObj>();

    private MainCanvases _mainCanvases;
        
    /// <summary>
    /// 생성된 룸에 대한 정보를 얻을 때 마다 업데이트 되는 함수, 룸리스트의 갯수만큼 프리팹 생성, 룸 정보 업데이트.
    /// </summary>
    /// <param name="roomList"></param>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");
        Debug.Log("roomList.cnt"+roomList.Count);
        foreach (var info in roomList)
        {
            if (info.RemovedFromList)
            {
                //로비에 있던 방 중에, 더이상 사용되지 않는 룸을 찾아 지운다.       
                int index = _objList.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index != -1)
                {
                    Destroy(_objList[index].gameObject);
                    _objList.RemoveAt(index);
                }
            }
            else
            {
                int index = _objList.FindIndex(x => x.RoomInfo.Name == info.Name);
                if (index == -1)
                {
                    //룸리스트에 추가한다
                    RoomObj go = Instantiate<RoomObj>(_prefab, _contents);
                    Debug.Log("go.name "+go.name);
                    if (go != null)
                    {
                        go.SetRoomInfo(info);
                        _objList.Add(go);
                    }
                }
            }
        }
    }


    public void Initialize(MainCanvases canvase)
    {
        _mainCanvases = canvase;
    }
    public override void OnJoinedRoom()
    {
        _mainCanvases.currenRoomMenu.gameObject.SetActive(true);
        _contents.DestroyChildren();
        _objList.Clear();
    }
    
}
