using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvases : MonoBehaviour
{
    //캔버스 두개를 관리하는 메인 캔버스.
    public CreateRoomMenu createRoomMenu = null;
    public CurrentRoomMenu currenRoomMenu = null;

    private void Awake()
    {
        FirstInitialize();
    }

    private void FirstInitialize()
    {
        createRoomMenu.Initialize(this);
        currenRoomMenu.Initialize(this);
    }
}
