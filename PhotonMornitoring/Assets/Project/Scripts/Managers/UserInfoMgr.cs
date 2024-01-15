using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class UserInfoMgr : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI userName;

    public void OnclickVerifiedBtn()
    {
        MasterManager.GameSettings.NickName = userName.text;
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        this.gameObject.SetActive(false);
    }
}
